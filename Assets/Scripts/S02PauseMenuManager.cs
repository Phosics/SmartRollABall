using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class S02PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject endGameMenu;
    public TextMeshProUGUI highScoreText;
    public AudioSource ambientAudioSource;
    
    private float _prevTimeScale;

    private void Start()
    {
        _prevTimeScale = Time.timeScale;
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);
    }

    private void OnPause()
    {
        pauseMenu.SetActive(true);
        StopTime();
    }
    
    public void OnResume()
    {
        pauseMenu.SetActive(false);
        ResumeTime();
    }

    public void OnEndGame()
    {
        endGameMenu.SetActive(true);
        highScoreText.text = "High score is " + PlayerPrefs.GetInt("high_score");
        StopTime();
    }
    
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeTime();
    }
    
    public void Quit()
    {
        if(EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }

    private void StopTime()
    {
        _prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
        ambientAudioSource.Pause();
    }
    
    private void ResumeTime()
    {
        Time.timeScale = _prevTimeScale;
        ambientAudioSource.UnPause();
    }
}
