using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class S01UIController : MonoBehaviour
{
    [FormerlySerializedAs("scoreManager")] public S01ScoreManager s01ScoreManager;
    [FormerlySerializedAs("applicationManager")] public S01ApplicationManager s01ApplicationManager;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    
    public void GameOver()
    {
        s01ScoreManager.GameOver();
        gameOverMenu.SetActive(true);
        s01ApplicationManager.Pause();
    }

    public void Resume()
    {
        s01ApplicationManager.Resume();
        pauseMenu.SetActive(false);
    }

    public void Retry()
    {
        s01ApplicationManager.Retry();
        gameOverMenu.SetActive(false);
    }
    
    private void Start()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void OnPause()
    {
        pauseMenu.SetActive(true);
        s01ApplicationManager.Pause();
    }
}
