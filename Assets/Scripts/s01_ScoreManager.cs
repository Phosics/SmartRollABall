using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public TextMeshProUGUI highScoreText;
    
    private int _score;

    public void Start()
    {
        Reset();
    }
    
    public void Increase()
    {
        _score++;
        SetCountText();
    }

    public void GameOver()
    {
        if (_score > GetHighScore())
        {
            SetHighScore(_score);
        }
        
        highScoreText.text = "High Score: " + GetHighScore();
    }

    public void Reset()
    {
        _score = 0;
        SetCountText();
    }

    private static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }

    private static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SetCountText()
    {
        countText.text = "Count: " + _score;
    }
}