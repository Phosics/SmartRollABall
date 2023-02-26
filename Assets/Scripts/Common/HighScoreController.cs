using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class HighScoreController : MonoBehaviour
    {
        [Header("Score")] 
        public ScoreManager scoreManager;
        
        [Space(5)]
        [Header("Players")] 
        public HighScorePlayerMenu[] highScorePlayers;
        
        private const string HighScoreNoPlayerName = "NO DATA";
        private static string GetTimeText(double secs) => (Math.Floor(secs * 100) / 100) + " seconds";

        private void Start()
        {
            if (highScorePlayers.Length != scoreManager.topPlaysAmount)
            {
                throw new Exception("Menu fields for high score players not like wanted amount");
            }
        }

        public void ViewScoreBoard(string playerName, double playTime)
        {
            ViewScoreBoard(scoreManager.UpdateHighScore(playerName, playTime));
        }
        
        public void ViewScoreBoard()
        {
            ViewScoreBoard(scoreManager.GetTopPlayersToPlayTime());
        }

        private void ViewScoreBoard(IReadOnlyCollection<KeyValuePair<string, double>> topPlayersToTimes)
        {
            Debug.Log("Setting high score for players");
            for (var i = 0; i < topPlayersToTimes.Count; i++)
            {
                SetHighScorePlayer(highScorePlayers[i], topPlayersToTimes.ElementAt(i));
            }
            Debug.Log(topPlayersToTimes.Count + " players set");
            
            Debug.Log("Setting empty score for empty players");
            for (var i = topPlayersToTimes.Count; i < highScorePlayers.Length ; i++)
            {
                SetEmptyHighScorePlayer(highScorePlayers[i]);
            }
            Debug.Log(highScorePlayers.Length - topPlayersToTimes.Count + " players set");
        }
        
        private static void SetHighScorePlayer(HighScorePlayerMenu highScorePlayerMenu, 
            KeyValuePair<string, double> keyValuePair)
        {
            highScorePlayerMenu.playerName.SetText(keyValuePair.Key);
            highScorePlayerMenu.playerTime.SetText(GetTimeText(keyValuePair.Value));
        }
        
        private static void SetEmptyHighScorePlayer(HighScorePlayerMenu highScorePlayerMenu)
        {
            highScorePlayerMenu.playerName.SetText(HighScoreNoPlayerName);
            highScorePlayerMenu.playerTime.SetText(GetTimeText(0.0f));
        }
    }
}