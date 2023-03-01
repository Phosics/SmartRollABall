using Common;
using Common.Menus;
using TMPro;
using UnityEditor.UI;
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
        public GameObject highScoreMenu;
        public HighScoreController highScoreController;

        [Space(5)] 
        [Header("Options")] 
        public GameObject gameTitle;
        public TMP_InputField inputField;
        public Toggle toggleAI;

        public void Start()
        {
            toggleAI.isOn = SceneSettings.useAI;
            toggleAI.onValueChanged.AddListener(ChooseAI);

            inputField.text = PlayerPrefs.GetString("current_player");
            inputField.onValueChanged.AddListener(ChangePlayerName);

            SetStartGamesInteractability();
        }

        public void ChangePlayerName(string playerName)
        {
            PlayerPrefs.SetString("current_player", playerName);
            SetStartGamesInteractability();
        }
        
        public void ChooseAI(bool withAi)
        {
            SceneSettings.useAI = withAi;
            PlayerPrefs.SetInt("is_ai", withAi ? 1 : 0);
            SetStartGamesInteractability();
        }

        private void SetStartGamesInteractability()
        {
            var canStartGame = !string.IsNullOrWhiteSpace(PlayerPrefs.GetString("current_player")) || PlayerPrefs.GetInt("is_ai") == 1;
            stage1.GetComponentInChildren<Button>().interactable = canStartGame;
            stage2.GetComponentInChildren<Button>().interactable = canStartGame;
            stage3.GetComponentInChildren<Button>().interactable = canStartGame;
        }

        public void StartStage1()
        {
            Debug.Log("Loading Stage 01");
            SetTimeScale();
            SceneManager.LoadScene("Stage - 01");
        }
    
        public void StartStage2()
        {
            Debug.Log("Loading Stage 02");
            SetTimeScale();
            SceneManager.LoadScene("Stage - 02");
        }
    
        public void StartStage3()
        {
            Debug.Log("Loading Stage 03");
            SetTimeScale();
            SceneManager.LoadScene("Stage - 03");
        }

        public void ShowScoreboard(int stageNumber)
        {
            Debug.Log("Showing the high score board");

            highScoreController.scoreManager.stage = (StageNumber)stageNumber;
            highScoreController.ViewScoreBoard();

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
            inputField.gameObject.SetActive(isActive);
            gameTitle.SetActive(isActive);
            highScoreMenu.SetActive(!isActive);
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
    }
}
