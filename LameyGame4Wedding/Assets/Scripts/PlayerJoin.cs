using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    public GameObject PlayerA;
    public GameObject PlayerB;
    [SerializeField] Image portrait1;
    [SerializeField] Image portrait2;
    [SerializeField] GameObject button;
    public void OnPlayerJoined(PlayerInput input)
    {
        // if(PlayerInputManager.instance.playerPrefab == PlayerA)
        // {
        //     PlayerInputManager.instance.playerPrefab = PlayerB;
        // }

        if(input.playerIndex >= 1){
            // MainCam.SetActive(false);
            portrait2.sprite = PlayerB.GetComponentInChildren<PlayerController>().portrait;
            GetComponent<StartRace>().p2 = PlayerB.GetComponentInChildren<PlayerController>();
            button.SetActive(true);
        }
        else{
            portrait1.sprite = PlayerA.GetComponentInChildren<PlayerController>().portrait;
            GetComponent<StartRace>().p1 = PlayerA.GetComponentInChildren<PlayerController>();            
        }

    }
}
