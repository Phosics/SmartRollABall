using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = System.Random;

public class S02ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private int _score = 0;

    private void Start()
    {
        SetCountText();
    }

    public void IncreaseScore()
    {
        _score++;
        PlayerPrefs.SetInt("high_score", Math.Max(PlayerPrefs.GetInt("high_score"), _score));
        SetCountText();
    }
    
    private void SetCountText()
    {
        countText.text = "Score: " + _score.ToString();
    }
}
