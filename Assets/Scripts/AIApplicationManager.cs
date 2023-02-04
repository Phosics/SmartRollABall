using UnityEngine;

public class AIApplicationManager : S01ApplicationManagerBase
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

    public override void Pause()
    {
        if (IsPaused())
        {
            return; 
        }
        
        Time.timeScale = 0;
    }

    public override void Resume()
    {
        if (!IsPaused())
        {
            return;
        }
        
        Time.timeScale = 1;
    }

    public override void Retry()
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
