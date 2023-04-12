using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
	[SerializeField] string HorizontalAxis;
	[SerializeField] string VerticalAxis;
    public Transform focus;
    Vector3 focusPoint, previousFocusPoint;
    public float distance = 5f;
    public float focusRadius = 1f;
    [SerializeField, Range(0f, 1f)]
	float focusCentering = 0.5f;
    Vector2 orbitAngles = new Vector2(45f, 0f);
    [SerializeField, Range(1f, 360f)]
	float rotationSpeed = 90f;
    [SerializeField, Range(-89f, 89f)]
	float minVerticalAngle = -30f, maxVerticalAngle = 60f;
    [SerializeField, Min(0f)]
	float alignDelay = 5f;
    float lastManualRotationTime;
    	[SerializeField, Range(0f, 90f)]
	float alignSmoothRange = 45f;
    Camera regularCamera;
    Vector3 CameraHalfExtends {
		get {
			Vector3 halfExtends;
			halfExtends.y =
				regularCamera.nearClipPlane *
				Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
			halfExtends.x = halfExtends.y * regularCamera.aspect;
			halfExtends.z = 0f;
			return halfExtends;
		}
	}
    [SerializeField]
	LayerMask obstructionMask = -1;
	private Vector2 movementInput = Vector2.zero;
    // Start is called before the first frame update
    void Awake()
    {
        regularCamera = GetComponent<Camera>();
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
    }
	public void OnCamera(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateFocusPoint();
        Quaternion lookRotation;
        if(ManualRotation() || AutomaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else{
            lookRotation = transform.localRotation;
        }

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;

        Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
		Vector3 rectPosition = lookPosition + rectOffset;
		Vector3 castFrom = focus.position;
		Vector3 castLine = rectPosition - castFrom;
		float castDistance = castLine.magnitude;
		Vector3 castDirection = castLine / castDistance;

        if (Physics.BoxCast(castFrom, CameraHalfExtends, castDirection, out RaycastHit hit, lookRotation, castDistance, obstructionMask)) {
            rectPosition = castFrom + castDirection * hit.distance;
			lookPosition = rectPosition - rectOffset;
		}

        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    void UpdateFocusPoint() {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;
		if (focusRadius > 0f) {
			float distance = Vector3.Distance(targetPoint, focusPoint);
			float t = 1f;
			if (distance > 0.01f && focusCentering > 0f) {
				t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
			}
			if (distance > focusRadius) {
				t = Mathf.Min(t, focusRadius / distance);
			}
			focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
		}
		else {
			focusPoint = targetPoint;
		}
    }

    bool ManualRotation() {
		Vector2 input = new Vector2(
			-1*movementInput.y,
			movementInput.x
		);
		const float e = 0.001f;
		if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
			orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledTime;
            return true;
		}
        return false;
	}

    bool AutomaticRotation () {
		if (Time.unscaledTime - lastManualRotationTime < alignDelay) {
			return false;
		}
        Vector2 movement = new Vector2(
			focusPoint.x - previousFocusPoint.x,
			focusPoint.z - previousFocusPoint.z
		);
		float movementDeltaSqr = movement.sqrMagnitude;
		if (movementDeltaSqr < 0.0001f) {
			return false;
		}
		float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
        float rotationChange = rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);
        if (deltaAbs < alignSmoothRange) {
			rotationChange *= deltaAbs / alignSmoothRange;
		}
        else if (180f - deltaAbs < alignSmoothRange) {
			rotationChange *= (180f - deltaAbs) / alignSmoothRange;
		}
		orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
		return true;
	}

    static float GetAngle (Vector2 direction) {
		float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
		return direction.x < 0f ? 360f - angle : angle;
	}

    void OnValidate () {
		if (maxVerticalAngle < minVerticalAngle) {
			maxVerticalAngle = minVerticalAngle;
		}
	}

    void ConstrainAngles () {
		orbitAngles.x =
			Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

		if (orbitAngles.y < 0f) {
			orbitAngles.y += 360f;
		}
		else if (orbitAngles.y >= 360f) {
			orbitAngles.y -= 360f;
		}
	}
}
