using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRaceTrigger : MonoBehaviour
{
    StartRace end;
    RaceManager manager;
    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<StartRace>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RaceManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cat")
        {
            Debug.Log("Fuck Unity");
            end.EndMovement();
            if(other.GetComponentInParent<SetLook>().jamie)
            {
                manager.EndGame("Jamie");
            }
            else{
                manager.EndGame("Lane");
            }
        }
    }
}
