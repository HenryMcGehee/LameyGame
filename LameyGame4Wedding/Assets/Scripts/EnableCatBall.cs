using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCatBall : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public void CatPowerOn(){
        rb.useGravity = true;
    }
    public void CatPowerOff(){
        rb.useGravity = false;
    }
}
