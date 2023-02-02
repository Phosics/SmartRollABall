using UnityEngine;

public class AIApplicationManager : MonoBehaviour
{
    public S01PlayerController playerController;
    public S01ScoreManager scoreManager;
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
        
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (!IsPaused())
        {
            return;
        }
        
        Time.timeScale = 1;
    }

    public void Retry()
    {
        playerController.Reset();
        scoreManager.Reset();
        pickupsController.Reset();
        Time.timeScale = 1;
    }

    private static bool IsPaused()
    {
        return Time.timeScale == 0;
    }
}
