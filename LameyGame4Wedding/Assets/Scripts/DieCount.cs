using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCount : MonoBehaviour
{
    [SerializeField] BattleGameManager manager;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cat")
        {
            Debug.Log("Fuck Unity");
            other.gameObject.GetComponent<PlayerController>().Respawn();
            
            if(other.gameObject.GetComponent<PlayerController>().parent.GetComponent<SetLook>().jamie)
            {
                manager.s2++;
            }
            else{
                manager.s1++;
            }
            manager.UpdateScore();
        }
    }
}
