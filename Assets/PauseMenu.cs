using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // brackeys tutorial: https://youtu.be/JivuXdrIHK0

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (TimeManager.Instance.GameIsPaused())
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
        TimeManager.Instance.Pause();
        pauseMenuUI.SetActive(true);
    }
    
    public void Resume()
    {
        Debug.Log("Resume");
        TimeManager.Instance.Resume();
        pauseMenuUI.SetActive(false);
    }

    public void LoadMenu()
    {
        TimeManager.Instance.Resume();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void RePlayGame()
    {
        TimeManager.Instance.Reset();
        SceneManager.LoadScene("Respawn");
    }
    public void ReplaySpecial()
    {
        TimeManager.Instance.Reset();
        SceneManager.LoadScene("RespawnSpecial");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
