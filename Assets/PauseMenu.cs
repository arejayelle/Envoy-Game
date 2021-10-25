using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // brackeys tutorial: https://youtu.be/JivuXdrIHK0

    public GameObject pauseMenuUI;
    public GameObject scoreCanvas;
    public GameObject roundEndCanvas;
    
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI RoundNumberText;

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
        scoreCanvas.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    
    public void Resume()
    {
        Debug.Log("Resume");
        TimeManager.Resume();
        scoreCanvas.SetActive(true);
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
        TimeManager.Pause();
        scoreCanvas.SetActive(false);

        RoundNumberText.text = $"Round {roundNumber} Completed";
        scoreText.text = ScoreManager.instance.GetScore().ToString();
        roundEndCanvas.SetActive(true);
    }

    public void OnRoundResume()
    {
        scoreCanvas.SetActive(true);
        roundEndCanvas.SetActive(false);
        TimeManager.Resume();
    }
}
