using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text t;
    [SerializeField] StartRace s;
    [SerializeField] GameObject manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
    }
    public IEnumerator Count()
    {
        t.text = "3";
        yield return new WaitForSeconds(1f);
        t.text = "2";
        yield return new WaitForSeconds(1f);
        t.text = "1";
        yield return new WaitForSeconds(1f);
        t.text = "Go!";
        for (int i = 0; i < s.players.Length; i++)
        {
            s.players[i].GetComponent<EnableCatBall>().CatPowerOn();
        }
        manager.SendMessage("StartGame");
        yield return new WaitForSeconds(1f);
        t.text = "";
        gameObject.SetActive(false);
    }
}
