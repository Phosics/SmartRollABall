using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common
{
    public class MainMenu : MonoBehaviour
    {
        public Toggle toggleAI;

        public void Start()
        {
            toggleAI.onValueChanged.AddListener(shouldUseAI => PlayerPrefs.SetInt("with_ai", shouldUseAI ? 1 : 0));
        }

        public void StartStage1()
        {
            SceneManager.LoadScene("Stage - 01");
        }
    
        public void StartStage2()
        {
            SceneManager.LoadScene("Stage - 02");
        }
    
        public void StartStage3()
        {
            SceneManager.LoadScene("Stage - 03");
        }
    }
}
