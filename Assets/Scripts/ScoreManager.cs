using TMPro;
using UnityEngine;

public enum StageNumber { One = 1, Two = 2, Three = 3, Test = 4 }

public class ScoreManager : MonoBehaviour
{
    public StageNumber stage;
    public int targetScore;
    
    [Space(5)]
    [Header("UI")]
    public TextMeshProUGUI countText;

    [Space(5)] [Header("Training")] 
    public bool isTraining;


    private int _score;
    
    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _score = 0;
        SetCountText();
    }

    public bool Increase()
    {
        _score++;
        UpdateHighScore();
        SetCountText();
        
        return _score >= targetScore;
    }
    
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(GetHighScoreText());
    }

    private void UpdateHighScore()
    {
        if (isTraining)
        {
            return;
        }
        
        if (_score > GetHighScore())
        {
            PlayerPrefs.SetInt(GetHighScoreText(), _score);
        }
    }

    private string GetHighScoreText()
    {
        return $"high_score{stage}";
    }

    private void SetCountText()
    {
        if (isTraining)
        {
            return;
        }
        
        countText.text = $"Score: {_score}";
    }
}
