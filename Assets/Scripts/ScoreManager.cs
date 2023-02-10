using System;
using TMPro;
using UnityEngine;

public enum StageNumber { One = 1, Two = 2, Three = 3 }

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public StageNumber stage;
    public int targetScore;

    private int _score = 0;
    
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
        PlayerPrefs.SetInt($"high_score_{stage}", Math.Max(PlayerPrefs.GetInt($"high_score{stage}"), _score));
        SetCountText();
        return _score >= targetScore;
    }
    
    private void SetCountText()
    {
        countText.text = $"Score: {_score}";
    }
}
