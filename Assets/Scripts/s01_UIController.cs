using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public ApplicationManager applicationManager;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    
    public void GameOver()
    {
        scoreManager.GameOver();
        gameOverMenu.SetActive(true);
        applicationManager.Pause();
    }

    public void Resume()
    {
        applicationManager.Resume();
        pauseMenu.SetActive(false);
    }

    public void Retry()
    {
        applicationManager.Retry();
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
        applicationManager.Pause();
    }
}
