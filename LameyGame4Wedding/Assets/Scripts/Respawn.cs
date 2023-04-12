using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    GameObject[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cat")
        {
            Debug.Log("Fuck Unity");
            other.gameObject.GetComponent<PlayerController>().Respawn();
        }
    }
}
