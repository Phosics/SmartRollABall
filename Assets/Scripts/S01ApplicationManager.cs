using UnityEngine;

public class S01ApplicationManager : MonoBehaviour
{
    public AudioSource ambientSound;
    public S01PlayerController playerController;
    public S01ObstaclesController obstaclesController;
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
        
        ambientSound.Pause();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (!IsPaused())
        {
            return;
        }
        
        ambientSound.UnPause();
        Time.timeScale = 1;
    }

    public void Retry()
    {
        playerController.Reset();
        obstaclesController.Reset();
        ambientSound.Play();
        scoreManager.Reset();
        pickupsController.Reset();
        Time.timeScale = 1;
    }

    private static bool IsPaused()
    {
        return Time.timeScale == 0;
    }
}
