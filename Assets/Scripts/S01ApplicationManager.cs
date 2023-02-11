using UnityEngine;

public class S01ApplicationManager : MonoBehaviour
{
    public S01PlayerController playerController;
    public S01ObstaclesController obstaclesController;
    public ScoreManager scoreManager;
    public S01PickupsController pickupsController;
    
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Pause()
    {
        if (IsPaused())
        {
            return; 
        }
        
        AudioManager.Pause("Theme");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (!IsPaused())
        {
            return;
        }
        
        AudioManager.UnPause("Theme");
        Time.timeScale = 1;
    }

    public void Retry()
    {
        playerController.Reset();
        obstaclesController.Reset();
        scoreManager.Reset();
        pickupsController.Reset();
        Time.timeScale = 1;
    }

    private static bool IsPaused()
    {
        return Time.timeScale == 0;
    }
}
