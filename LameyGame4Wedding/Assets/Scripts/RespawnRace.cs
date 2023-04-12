using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRace : MonoBehaviour
{
    GameObject[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cat")
        {
            Debug.Log("Fuck Unity");
            
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().MovePosition(FindRespawn(other.transform));
        }
    }
    Vector3 FindRespawn(Transform player)
    {
        GameObject t = spawns[0];
        for (int i = 0; i < spawns.Length; i++)
        {
            if(Vector3.Distance(spawns[i].transform.position, player.position) < Vector3.Distance(t.transform.position, player.position))
            {
                t = spawns[i];
            }
        }
        return t.transform.position;
    }
}
