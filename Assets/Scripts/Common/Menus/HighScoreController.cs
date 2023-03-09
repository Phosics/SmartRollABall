using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Menus
{
    public class HighScoreController : MonoBehaviour
    {
        [Header("Score")] 
        public ScoreManager scoreManager;

        [Space(5)]
        // public HighscoreTable HighscoreTable;
        //[Header("AI Player")] 
        //public HighScorePlayerMenu highScoreAIPlayer;
        
        //[Space(2)]
        //[Header("Players")] 
        //public HighScorePlayerMenu[] highScorePlayers;
        
        private const string HighScoreNoPlayerName = "NO DATA";
        private static string GetTimeText(double secs) => (Math.Floor(secs * 100) / 100) + " seconds";

        private void Start()
        {
            //HighscoreTable.gameObject.SetActive(false);
            //if (highScorePlayers.Length != scoreManager.topPlaysAmount)
            //{
            //    throw new Exception("Menu fields for high score players not like wanted amount");
            //}

        }

        public void ViewScoreBoard(double playTime)
        {
            if (SceneSettings.useAI)
            {
                scoreManager.SetAIPlayerHighScore(playTime);
                ViewScoreBoard();
            }
            else
            {
                ViewScoreBoard(scoreManager.UpdateHighScore(playTime));
            }
        }
        
        public void ViewScoreBoard()
        {
            ViewScoreBoard(scoreManager.GetTopPlayersToPlayTime());
        }

        private void ViewScoreBoard(IReadOnlyCollection<KeyValuePair<string, double>> topPlayersToTimes)
        {
            // Setting the AI Player high score
            //highScoreAIPlayer.playerName.SetText("AI");
            //highScoreAIPlayer.playerTime.SetText(GetTimeText(scoreManager.GetAIPlayerHighScore()));

            //// Setting the manual players high score 
            //for (var i = 0; i < topPlayersToTimes.Count; i++)
            //{
            //    SetHighScorePlayer(highScorePlayers[i], topPlayersToTimes.ElementAt(i));
            //}

            //for (var i = topPlayersToTimes.Count; i < highScorePlayers.Length ; i++)
            //{
            //    SetEmptyHighScorePlayer(highScorePlayers[i]);
            //}
            // HighscoreTable.gameObject.SetActive(true);
        }
        
        private static void SetHighScorePlayer(HighScorePlayerMenu highScorePlayerMenu, 
            KeyValuePair<string, double> keyValuePair)
        {
            //highScorePlayerMenu.playerName.SetText(keyValuePair.Key);
            //highScorePlayerMenu.playerTime.SetText(GetTimeText(keyValuePair.Value));
        }
        
        private static void SetEmptyHighScorePlayer(HighScorePlayerMenu highScorePlayerMenu)
        {
            //highScorePlayerMenu.playerName.SetText(HighScoreNoPlayerName);
            //highScorePlayerMenu.playerTime.SetText(GetTimeText(0.0f));
        }
    }
}