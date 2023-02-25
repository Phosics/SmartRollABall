using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common
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
            toggleAI.onValueChanged.AddListener(shouldUseAI => PlayerPrefs.SetInt("with_ai", shouldUseAI ? 1 : 0));
        }

        public void StartStage1()
        {
            SetTimeScale();
            SceneManager.LoadScene("Stage - 01");
        }
    
        public void StartStage2()
        {
            SetTimeScale();
            SceneManager.LoadScene("Stage - 02");
        }
    
        public void StartStage3()
        {
            SetTimeScale();
            SceneManager.LoadScene("Stage - 03");
        }

        public void ShowScoreboard(int stageNumber)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(false);
            toggleAI.gameObject.SetActive(false);

            switch (stageNumber)
            {
                case 1:
                    highScoreController.scoreManager.stage = StageNumber.One;
                    break;
                case 2:
                    highScoreController.scoreManager.stage = StageNumber.Two;
                    break;
                case 3:
                    highScoreController.scoreManager.stage = StageNumber.Three;
                    break;
            }
            
            highScoreController.ViewScoreBoard();
            
            highScoreMenu.SetActive(true);
        }

        public void SuppressScoreboard()
        {
            stage1.SetActive(true);
            stage2.SetActive(true);
            stage3.SetActive(true);
            toggleAI.gameObject.SetActive(true);
            
            highScoreMenu.SetActive(false);
        }

        private static void SetTimeScale()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
