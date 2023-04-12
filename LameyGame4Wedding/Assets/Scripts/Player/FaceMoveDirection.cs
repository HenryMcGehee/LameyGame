using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FaceMoveDirection : MonoBehaviour
{
    public float rotationSpeed;
    [SerializeField] private Camera cam;
    private Quaternion lookRotation;
    private Vector3 direction;
    private Vector3 movementInput;
    // Start is called before the first frame update
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = movementInput.x;
        float v = movementInput.y;
        
        Quaternion r = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        Vector3 move = new Vector3(h, 0,v);
        Vector3 final = r * move;

        direction = (move - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(final);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);   
    }
}
