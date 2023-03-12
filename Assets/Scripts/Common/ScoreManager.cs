using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Common
{
    public enum StageNumber { One = 1, Two = 2, Three = 3, Test = 4 }

    public class ScoreManager : MonoBehaviour
    {
        public StageNumber stage;
        public int targetScore;

        [Space(5)]
        [Header("UI")]
        public TextMeshProUGUI countText;

        private int _score;

        private void Start()
        {
            var targetScorePrefValue = PlayerPrefs.GetInt("target_score", -1);
            if (targetScorePrefValue != -1)
                targetScore = targetScorePrefValue;

            PlayerPrefs.SetInt("target_score", targetScore);

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
            SetCountText();
        
            return _score >= targetScore;
        }

        private void SetCountText()
        {
            if (countText != null)
                countText.text = $"Score: {_score} / {targetScore}";
        }
    }
}