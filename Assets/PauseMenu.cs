using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // brackeys tutorial: https://youtu.be/JivuXdrIHK0

    public GameObject pauseMenuUI;
    public GameObject InGameUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (TimeManager.GameIsPaused())
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        TimeManager.Pause();
        InGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    
    public void Resume()
    {
        Debug.Log("Resume");
        TimeManager.Resume();
        InGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void LoadMenu()
    {
        TimeManager.Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void OnRoundEnd(int roundNumber)
    {
        RoundManager.instance.NewRound();
    }

    public void OnRoundResume()
    {
        InGameUI.SetActive(true);
        TimeManager.Resume();
    }
}
