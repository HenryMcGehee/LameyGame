using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    public PlayerController p1;
    public PlayerController p2;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject startUI;
    public GameObject[] players;
    [SerializeField] CountDown cd;
    GameObject[] spawns;
    void Start(){
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }
    public void Begin()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        players[0].GetComponent<SetLook>().jamie = true;
        players[0].GetComponent<SetLook>().UpdateLook();
        players[0].transform.position = spawns[0].transform.position;
        // players[0].transform.rotation = Quaternion.Euler(spawns[0].transform.rotation.x, spawns[0].transform.rotation.y, spawns[0].transform.rotation.z);
        players[1].GetComponent<SetLook>().jamie = false;
        players[1].GetComponent<SetLook>().UpdateLook();
        players[1].transform.position = spawns[1].transform.position;
        // players[1].transform.rotation = Quaternion.Euler(spawns[1].transform.rotation.x, spawns[1].transform.rotation.y, spawns[1].transform.rotation.z);
        cam.SetActive(false);
        startUI.SetActive(false);
        cd.StartCoroutine("Count");
    }
    public void EndMovement()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<EnableCatBall>().CatPowerOff();
        }
    }
}
