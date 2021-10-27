using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // brackeys tutorial: https://youtu.be/JivuXdrIHK0

    public GameObject pauseMenuUI;

    
    // controller nav https://www.youtube.com/watch?v=SXBgBmUcTe0&ab_channel=gamesplusjames
    public GameObject pauseFirstButton, loseFirstButton;
    
    
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
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        
        pauseMenuUI.SetActive(true);
    }
    
    public void Resume()
    {
        Debug.Log("Resume");
        TimeManager.Instance.Resume();
        EventSystem.current.SetSelectedGameObject(null);
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
