using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ApplicationManager : MonoBehaviour
{
    public AudioSource ambientSound;
    public PlayerController playerController;
    public ObstaclesController obstaclesController;
    public ScoreManager scoreManager;
    public PickupsController pickupsController;
    
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
