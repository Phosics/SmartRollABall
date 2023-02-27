using System;
using System.Collections.Generic;
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
        public int topPlaysAmount;

        [Space(5)]
        [Header("UI")]
        public TextMeshProUGUI countText;

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
            SetCountText();
        
            return _score >= targetScore;
        }

        public List<KeyValuePair<string, double>> UpdateHighScore(double playTime)
        {
            var currentTop = GetTopPlayersToPlayTime();
            
            var playerName = PlayerPrefs.GetString("current_player") ?? "DEFAULT_PLAYER";
            currentTop.Add(new KeyValuePair<string, double>(playerName, playTime));

            var newTop = currentTop
                .OrderBy(kvp => kvp.Value)
                .Take(topPlaysAmount);

            var newTopString = string.Join(Environment.NewLine, newTop.Select(kvp => kvp.Key + "=" + kvp.Value));
            
            PlayerPrefs.SetString(GetTopPlayersKey(), newTopString);
            
            return newTop.ToList();
        }
        
        public List<KeyValuePair<string, double>> GetTopPlayersToPlayTime()
        {
            var topString = PlayerPrefs.GetString(GetTopPlayersKey());

            if (string.IsNullOrEmpty(topString))
            {
                return new List<KeyValuePair<string, double>>();   
            }

            return topString.Split(Environment.NewLine).Select(playerToTimeString => 
                {
                    var separatorIndex = playerToTimeString.LastIndexOf('=');
                    var playerName = playerToTimeString[..separatorIndex];
                    var playTime = double.Parse(playerToTimeString[(separatorIndex + 1)..]);
                    return new KeyValuePair<string, double>(playerName, playTime);
                }).ToList();
        }

        private string GetTopPlayersKey()
        {
            return $"top_players{stage}";
        }

        private void SetCountText()
        {
            if (countText == null)
            {
                return;   
            }

            countText.text = $"Score: {_score} / {targetScore}";
        }
    }
}