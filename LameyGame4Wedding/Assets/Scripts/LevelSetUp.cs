using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSetUp : MonoBehaviour
{
    [SerializeField] GameObject ui;

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }
}
