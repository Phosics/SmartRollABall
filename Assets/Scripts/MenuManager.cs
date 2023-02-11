using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject endGameMenu;
    
    [Space(5)]
    [Header("UI")]
    public TextMeshProUGUI highScoreText;

    private ScoreManager _scoreManager;
    private float _prevTimeScale;

    private void Start()
    {
        _scoreManager = GetComponent<ScoreManager>();
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
        highScoreText.text = "High score is " + _scoreManager.GetHighScore();
        StopTime();
    }
    
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeTime();
    }
    
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = _prevTimeScale;
    }

    private void StopTime()
    {
        _prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
        AudioManager.Pause("Theme");
    }
    
    private void ResumeTime()
    {
        Time.timeScale = _prevTimeScale;
        AudioManager.UnPause("Theme");
    }
}
