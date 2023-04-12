using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform parent;
    public Sprite portrait;
    private Rigidbody rb;
    public Animator anim;
    [SerializeField] private Camera cam;
    public float moveSpeed;
    public float lookSpeed;
    public float jumpSpeed;
    public float _maxAngularVelocity;
    public float acceleration;
    public float maxSpeed;
    public bool canMove;
    
    float groundCheck;
    public bool grounded;
    int velocity;
    [SerializeField] private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    GameObject[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = _maxAngularVelocity;
        velocity = Animator.StringToHash("Move");
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            float h = movementInput.x;
            float v = movementInput.y;
            
            Quaternion r = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
            Vector3 move = new Vector3(v, 0, -1 * h);
            Vector3 final = r * move;
            if(movementInput.x != 0 || movementInput.y != 0)
            {
                if(moveSpeed < maxSpeed)
                    moveSpeed += acceleration;
            }
            else{
                moveSpeed = 100;
            }
            rb.AddTorque(final * moveSpeed * Time.deltaTime);

            // Animation Controls

            float vel = Mathf.Clamp(rb.velocity.magnitude * 0.2f, 0f, 1f);

            anim.SetFloat(velocity, vel);
            
            if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), 2f))
            {
                grounded = true;
                anim.SetBool("Grounded", true);
            }
            else{
                grounded = false;
                anim.SetBool("Grounded", false);
            }

            if(jumped && grounded)
            {
                Debug.Log("jumped");
                anim.Play("Jump");
                rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed);
            }
        }
    }

    public void Respawn()
    {
        Debug.Log(" fucccc");
        rb.MovePosition(spawns[Random.Range(0, spawns.Length)].transform.position);
        // canMove = false;
        // yield return new WaitForSeconds(0.1f);
        // parent.position = spawns[Random.Range(0, spawns.Length)].transform.position;
        // yield return new WaitForSeconds(0.1f);
        // canMove = true;
    }
}
