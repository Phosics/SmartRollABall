using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class S01ApplicationManager : MonoBehaviour
{
    public AudioSource ambientSound;
    [FormerlySerializedAs("playerController")] public S01PlayerController s01PlayerController;
    [FormerlySerializedAs("obstaclesController")] public S01ObstaclesController s01ObstaclesController;
    [FormerlySerializedAs("scoreManager")] public S01ScoreManager s01ScoreManager;
    [FormerlySerializedAs("pickupsController")] public S01PickupsController s01PickupsController;
    
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
        s01PlayerController.Reset();
        s01ObstaclesController.Reset();
        ambientSound.Play();
        s01ScoreManager.Reset();
        s01PickupsController.Reset();
        Time.timeScale = 1;
    }

    private static bool IsPaused()
    {
        return Time.timeScale == 0;
    }
}
