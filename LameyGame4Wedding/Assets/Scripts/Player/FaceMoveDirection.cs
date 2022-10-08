using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMoveDirection : MonoBehaviour
{
    public float rotationSpeed;
    private Camera cam;
    private Quaternion lookRotation;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Quaternion r = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        Vector3 move = new Vector3(h, 0,v);
        Vector3 final = r * move;

        direction = (move - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(final);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);   
    }
}
