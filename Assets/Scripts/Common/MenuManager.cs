using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class MenuManager : TrainLogicable
    {
        [Header("Menus")]
        public GameObject pauseMenu;
        public GameObject endGameMenu;
    
        [Space(5)]
        [Header("UI")]
        public TextMeshProUGUI player1Name;
        public TextMeshProUGUI player2Name;
        public TextMeshProUGUI player3Name;
        public TextMeshProUGUI player1Time;
        public TextMeshProUGUI player2Time;
        public TextMeshProUGUI player3Time;
        public TextMeshProUGUI endScreenTitle;
        public TextMeshProUGUI replayButtonText;

        private ScoreManager _scoreManager;
        private float _prevTimeScale;
        
        private float _playTimeInSecs = 0;
        private float _lastTimedTime;

        private void Start()
        {
            _scoreManager = GetComponent<ScoreManager>();
            _prevTimeScale = Time.timeScale;
            pauseMenu.SetActive(false);
            endGameMenu.SetActive(false);
            ResetLastTimedTime();
        }

        private void OnPause()
        {
            if (trainingMode)
                return;

            pauseMenu.SetActive(true);
            StopTime();
        }
    
        public void OnResume()
        {
            if (trainingMode)
                return;

            pauseMenu.SetActive(false);
            ResumeTime();
            ResetLastTimedTime();
        }

        public void OnLoseGame()
        {
            endScreenTitle.text = "YOU LOST!";
            replayButtonText.text = "Try again";
            OnEndGame();
        }
        
        public void OnWinGame()
        {
            endScreenTitle.text = "YOU WON!";
            replayButtonText.text = "Play again";
            OnEndGame();
        }
        
        private void OnEndGame(string winnerText = null)
        {
            if (trainingMode)
                return;

            StopTime();
            endGameMenu.SetActive(true);
            
            var topPlayersToTimes = string.IsNullOrEmpty(winnerText) ?
                _scoreManager.GetTopPlayersToPlayTime() :
                _scoreManager.UpdateAndGetTopPlayersToPlayTime("USER NAME", _playTimeInSecs);

            string GetTimeText(double secs) => (Math.Floor(secs * 100) / 100) + " seconds";

            var firstElement = topPlayersToTimes.ElementAt(0);
            player1Name.text = firstElement.Key;
            player1Time.text = GetTimeText(firstElement.Value);
            
            var secondElement = topPlayersToTimes.ElementAt(1);
            if (secondElement.Equals(default(KeyValuePair<string, double>)))
            {
                player2Name.enabled = false;
                player3Name.enabled = false;
                player2Time.enabled = false;
                player3Time.enabled = false;
                return;
            }

            player2Name.text = secondElement.Key;
            player2Time.text = GetTimeText(secondElement.Value);

            var thirdElement = topPlayersToTimes.ElementAt(2);
            if (thirdElement.Equals(default(KeyValuePair<string, double>)))
            {
                player3Name.enabled = false;
                player3Time.enabled = false;
                return;
            }
            
            player3Name.text = thirdElement.Key;
            player3Time.text = GetTimeText(thirdElement.Value);
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
            AddTimeSinceLastTimedTime();
            _prevTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioManager.Pause("Theme");
        }
    
        private void ResumeTime()
        {
            ResetLastTimedTime();
            Time.timeScale = _prevTimeScale;
            AudioManager.UnPause("Theme");
        }
        
        private void ResetLastTimedTime()
        {
            _lastTimedTime = Time.time;
        }
        
        private void AddTimeSinceLastTimedTime()
        {
            _playTimeInSecs += Time.time - _lastTimedTime;
        }
    }
}
