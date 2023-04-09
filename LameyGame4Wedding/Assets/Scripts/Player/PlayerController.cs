using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string moveX;
    [SerializeField] string moveY;
    [SerializeField] string jumpInput;
    private Rigidbody rb;
    public Animator anim;
    private Camera cam;
    public float moveSpeed;
    public float lookSpeed;
    public float jumpSpeed;
    public float _maxAngularVelocity;
    public float acceleration;
    public float maxSpeed;
    
    float groundCheck;
    public bool grounded;
    int velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = _maxAngularVelocity;
        velocity = Animator.StringToHash("Move");
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        // Physics Movement
        
        float h = Input.GetAxisRaw(moveX);
        float v = Input.GetAxisRaw(moveY);
        
        Quaternion r = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        Vector3 move = new Vector3(v, 0, -1 * h);
        Vector3 final = r * move;
        if(Input.GetAxisRaw(moveX) != 0 || Input.GetAxisRaw(moveY) != 0)
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

        if(Input.GetButtonDown(jumpInput) && grounded)
        {
            Debug.Log("jumped");
            anim.Play("Jump");
            rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed);
        }

    }
}
