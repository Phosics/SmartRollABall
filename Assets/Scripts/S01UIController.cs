using UnityEngine;

public class S01UIController : MonoBehaviour
{
    public S01ScoreManager scoreManager;
    public S01ApplicationManager applicationManager;
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
