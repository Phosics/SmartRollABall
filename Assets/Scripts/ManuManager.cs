using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ManuManager : MonoBehaviour
{
    public Canvas GamePlayCanvas;
    public Canvas PauseCanvas;
    public Canvas LossCanvas;

    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas.enabled = false;
        LossCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LossTriggerEvent()
    {
        Time.timeScale = 0;
        GamePlayCanvas.enabled = false;
        LossCanvas.enabled = true;
        AudioManager.Pause("Theme");
    }

    public void OnPause(InputAction.CallbackContext pause)
    {
        Debug.Log("OnPause");
        Time.timeScale = 0;
        GamePlayCanvas.enabled = false;
        PauseCanvas.enabled = true;
        AudioManager.Pause("Theme");
    }

    public void OnClickResume()
    {
        if (Time.timeScale == 0)
        {
            PauseCanvas.enabled = false;
            GamePlayCanvas.enabled = true;
            AudioManager.Play("Theme");
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogError("OnClickResume");
        }
    }

    public void OnClickExit()
    {
        if (Time.timeScale == 0)
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else
        {
            Debug.LogError("OnClickExit");
        }
    }

    public void OnRestart()
    {
        Time.timeScale = 1;
        AudioManager.Play("Theme");
        GamePlayCanvas.enabled = true;
        SceneManager.LoadScene("JumpingScene");
    }
}
