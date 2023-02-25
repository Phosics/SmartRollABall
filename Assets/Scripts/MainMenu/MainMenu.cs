using Common;
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
        public Toggle toggleAI;

        public void Start()
        {
            toggleAI.onValueChanged.AddListener(shouldUseAI => SceneSettings.useAI = shouldUseAI);
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
