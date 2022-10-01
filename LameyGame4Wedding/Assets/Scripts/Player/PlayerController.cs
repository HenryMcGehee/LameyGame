using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    public float moveSpeed;
    public float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Quaternion r = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        Vector3 move = new Vector3(v  * Time.deltaTime, 0, -1 * h  * Time.deltaTime);
        Vector3 final = r * move;
        rb.AddTorque(Vector3.Normalize(final) * moveSpeed);

        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumped");
            rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed);
        }

    }
}
