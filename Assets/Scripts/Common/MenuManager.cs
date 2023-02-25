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
        public GameObject highScoreMenu;
    
        [Space(5)]
        [Header("UI")]
        public TextMeshProUGUI endScreenTitle;
        public TextMeshProUGUI replayButtonText;

        private const int NormalTimeScale = 1;
        
        private HighScoreController _highScoreController;

        private float _playTimeInSecs = 0;
        private float _lastTimedTime;

        private void Start()
        {
            _highScoreController = GetComponent<HighScoreController>();

            highScoreMenu.SetActive(false);
            pauseMenu.SetActive(false);
            endGameMenu.SetActive(false);
            
            ResetLastTimedTime();
        }

        private void OnPause()
        {
            Debug.Log("Game paused");
            pauseMenu.SetActive(true);
            StopTime();
        }
    
        public void OnResume()
        {
            Debug.Log("Game resumed");
            pauseMenu.SetActive(false);
            ResumeTime();
            ResetLastTimedTime();
        }

        public void OnLoseGame()
        {
            Debug.Log("Game lost");
            endScreenTitle.text = "YOU LOST!";
            replayButtonText.text = "Try again";
            OnEndGame(false);
        }
        
        public void OnWinGame()
        {
            Debug.Log("Game won");
            endScreenTitle.text = "YOU WON!";
            replayButtonText.text = "Play again";
            OnEndGame(true);
        }
        
        private void OnEndGame(bool won)
        {
            if (trainingMode)
                return;

            StopTime();
            EnableEndGameMenu();

            if (won)
            {
                _highScoreController.ViewScoreBoard("Test Player", _playTimeInSecs);
            }
            else
            {
                _highScoreController.ViewScoreBoard();
            }

        }
    
        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ResumeTime();
        }
    
        public void Quit()
        {
            SceneManager.LoadScene("Main Menu");
        }

        private void StopTime()
        {
            AddTimeSinceLastTimedTime();
            Time.timeScale = 0;
            AudioManager.Pause("Theme");
        }
    
        private void ResumeTime()
        {
            ResetLastTimedTime();
            Time.timeScale = NormalTimeScale;
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
        
        private void EnableEndGameMenu()
        {
            endGameMenu.SetActive(true);
            highScoreMenu.SetActive(true);
        }
    }
}
