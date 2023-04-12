using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BattleGameManager : MonoBehaviour
{
    [SerializeField] Text score1;
    [SerializeField] Text score2;
    [SerializeField] Text timer;
    [SerializeField] GameObject endButton;
    public float timeRemaining;
    public int s1;
    public int s2;
    bool start;
    StartRace playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<StartRace>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timer.text = DisplayTime() + " Remaining!";
            }
            else{
                EndGame();
            }
        }
    }
    public void UpdateScore()
    {
        score1.text = "Score: " + s1.ToString();
        score2.text = "Score: " + s2.ToString();
    }
    public void StartGame()
    {
        start = true;
    }
    string FindWinner()
    {
        if(s1 > s2)
        {
            return "Jamie Wins!";
        }
        else if(s2>s1)
        {
            return "Lane Wins";
        }
        else{
            return "Tie!";
        }
    }
    string DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        return  minutes.ToString() + ":" + seconds.ToString();
    }
    void EndGame()
    {
        Debug.Log("GameOver");
        playerManager.EndMovement();
        start = false;
        endButton.SetActive(true);
        GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(endButton);
        timer.text = FindWinner();
    }
    public void ReturnToLevelSelect()
    {
        SceneManager.LoadSceneAsync("LevelSelect");
    }
}
