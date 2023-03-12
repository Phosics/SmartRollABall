using Common;
using Common.Menus;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Stages")] 
        public GameObject stage1;
        public GameObject stage2;
        public GameObject stage3;
        
        [Space(5)]
        [Header("High Score")]
        public HighscoreTable highScoreTable;

        [Space(5)] 
        [Header("Options")] 
        public GameObject gameTitle;
        public TMP_InputField playerName;
        public Toggle toggleAI;
        public TMP_Dropdown aiBrainOptions;
        public GameObject eagle;
        public TMP_InputField targetScore;
        public GameObject exitButton;

        public void Start()
        {
            toggleAI.isOn = SceneSettings.useAI;
            aiBrainOptions.interactable = SceneSettings.useAI;
            aiBrainOptions.value = SceneSettings.brain;
            playerName.text = PlayerPrefs.GetString("current_player", "Unknown");
            targetScore.text = PlayerPrefs.GetInt("target_score", 5).ToString();
            SetStartGamesInteractability();
        }

        public void OnPlayerNameChange()
        {
            PlayerPrefs.SetString("current_player", playerName.text);
            SetStartGamesInteractability();
        }

        public void OnTargetScoreChange()
        {
            var value = 0;

            var parsed = int.TryParse(targetScore.text, out value);

            if (parsed)
            {
                PlayerPrefs.SetInt("target_score", int.Parse(targetScore.text));
            }
            else
            {
                targetScore.text = "1";
            }
        }
        
        public void OnAIValueChange()
        {
            var value = toggleAI.isOn;
            SceneSettings.useAI = value;
            aiBrainOptions.interactable = value;
            PlayerPrefs.SetInt("is_ai", value ? 1 : 0);
            SetStartGamesInteractability();
        }

        public void OnBrainChange()
        {
            SceneSettings.brain = aiBrainOptions.value;
        }

        private void SetStartGamesInteractability()
        {
            var canStartGame = !string.IsNullOrWhiteSpace(PlayerPrefs.GetString("current_player")) || PlayerPrefs.GetInt("is_ai") == 1;
            stage1.GetComponentInChildren<Button>().interactable = canStartGame;
            stage2.GetComponentInChildren<Button>().interactable = canStartGame;
            stage3.GetComponentInChildren<Button>().interactable = canStartGame;
            // Debug.Log("canStartGame = " + canStartGame);
        }

        public void StartStage1()
        {
            PlayerPrefs.SetString("stage_number", "1");
            Debug.Log("Loading Stage 01");
            SetTimeScale();
            SceneManager.LoadScene("Stage - 01");
        }
    
        public void StartStage2()
        {
            PlayerPrefs.SetString("stage_number", "2");
            Debug.Log("Loading Stage 02");
            SetTimeScale();
            SceneManager.LoadScene("Stage - 02");
        }
    
        public void StartStage3()
        {
            PlayerPrefs.SetString("stage_number", "3");
            Debug.Log("Loading Stage 03");
            SetTimeScale();
            SceneManager.LoadScene("Stage - 03");
        }

        public void ShowScoreboard(int stageNumber)
        {
            Debug.Log("Showing the high score board for stage " + stageNumber);
            PlayerPrefs.SetString("stage_number", stageNumber.ToString());
            SetActiveToUIElements(false);
        }

        public void SuppressScoreboard()
        {
            Debug.Log("Suppressing the high score board");
            SetActiveToUIElements(true);
        }

        private void SetActiveToUIElements(bool isActive)
        {
            stage1.SetActive(isActive);
            stage2.SetActive(isActive);
            stage3.SetActive(isActive);
            toggleAI.gameObject.SetActive(isActive);
            playerName.gameObject.SetActive(isActive);
            targetScore.gameObject.SetActive(isActive);
            gameTitle.SetActive(isActive);
            aiBrainOptions.gameObject.SetActive(isActive);
            exitButton.SetActive(isActive);
            eagle.SetActive(isActive);
            highScoreTable.gameObject.SetActive(!isActive);
        }

        private static void SetTimeScale()
        {
            if (Time.timeScale != 0)
            {
                return;
            }
            
            Debug.Log("Time scale is 0, Changing it to 1");
            Time.timeScale = 1;
        }

        public void OnClickExit()
        {
            #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
