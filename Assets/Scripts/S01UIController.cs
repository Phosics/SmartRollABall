using UnityEngine;

public class S01UIController : MonoBehaviour
{
    public S01ScoreManager scoreManager;
    public S01ApplicationManagerBase applicationManagerBase;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    
    public void GameOver()
    {
        scoreManager.GameOver();
        gameOverMenu.SetActive(true);
        applicationManagerBase.Pause();
    }

    public void Resume()
    {
        applicationManagerBase.Resume();
        pauseMenu.SetActive(false);
    }

    public void Retry()
    {
        applicationManagerBase.Retry();
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
        applicationManagerBase.Pause();
    }
}
