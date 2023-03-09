using Common.Effects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Menus
{
    public class MenuManager : TrainLogicable
    {
        [Header("Menus")]
        public GameObject pauseMenu;
        public GameObject endGameMenu;
    
        [Space(5)]
        [Header("UI")]
        public TextMeshProUGUI endScreenTitle;
        public TextMeshProUGUI replayButtonText;
        public Timer timer;

        [Space(5)] 
        [Header("Other")] 
        public PlayGround playGround;

        private const int NormalTimeScale = 1;
        
        // private HighScoreController _highScoreController;
        private HighscoreTable _HighscoreTable;

        // private float _playTimeInSecs = 0;
        // private float _lastTimedTime;

        private void Start()
        {
            // _highScoreController = GetComponent<HighScoreController>();
            _HighscoreTable = GetComponentInChildren<HighscoreTable>();

            pauseMenu.SetActive(false);
            endGameMenu.SetActive(false);
            _HighscoreTable.gameObject.SetActive(false);

            timer.ResetTimer();
            timer.StartTimer();
            // ResetLastTimedTime();
        }

        public void OnPause()
        {
            Debug.Log("Game paused");
            pauseMenu.SetActive(true);
            timer.StopTimer();
            Time.timeScale = 0;
        }
    
        public void OnResume()
        {
            Debug.Log("Game resumed");
            pauseMenu.SetActive(false);
            _HighscoreTable.gameObject.SetActive(false);
            timer.ResumeTimer();
            Time.timeScale = NormalTimeScale;
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
            timer.StopTimer();
            Time.timeScale = 0;
            // StopTime();
            SetActiveEndGameMenu(true);

            if (won)
            {
                name = SceneSettings.useAI ? "Brain_" + (SceneSettings.brain + 1) : PlayerPrefs.GetString("current_player") ?? "DEFAULT_PLAYER";
                if (name == "")
                    name = "Unknown";

                _HighscoreTable.AddHighscoreEntry(timer.TimeInGame, name);
            }
                // _highScoreController.ViewScoreBoard(timer.TimeInGame);
                // _highScoreController.ViewScoreBoard(_playTimeInSecs);
            _HighscoreTable.gameObject.SetActive(true);
            // _highScoreController.ViewScoreBoard();

        }
    
        // Reset the playground without loading the scene
        public void ResetGameOnEnd()
        {
            SetActiveEndGameMenu(false);
            ResetGame();
        }

        public void ResetGameOnMiddle()
        {
            if (SceneSettings.useAI)
            {
                playGround.playerManager.EndEpisode();
            }

            pauseMenu.SetActive(false);
            _HighscoreTable.gameObject.SetActive(false);
            ResetGame();
        }

        private void ResetGame()
        {
            playGround.ResetPlayGround();
            playGround.particlesEffector.StopEffect();
            playGround.postProcessingEffector.StopEffect();
            _HighscoreTable.gameObject.SetActive(false);
            timer.ResetTimer();
            timer.StartTimer();
            Time.timeScale = NormalTimeScale;
            // ResumeTime();
        }
        
    
        public void Quit()
        {
            AudioManager.Pause("Theme", false);
            SceneManager.LoadScene("Main Menu");
        }

        private void SetActiveEndGameMenu(bool enable)
        {
            endGameMenu.SetActive(enable);
        }
    }
}
