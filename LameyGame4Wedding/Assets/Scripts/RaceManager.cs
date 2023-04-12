using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    bool start;
    [SerializeField] GameObject endButton;
    [SerializeField] Text timer;
    public void StartGame()
    {
        start = true;
    }
    public void EndGame(string winnerName)
    {
        Debug.Log("GameOver");
        start = false;
        endButton.SetActive(true);
        GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(endButton);
        timer.text = winnerName + " Wins!";
    }
    public void ReturnToLevelSelect()
    {
        SceneManager.LoadSceneAsync("LevelSelect");
    }
}
