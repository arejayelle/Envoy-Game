using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private static bool _gameIsPaused;
    private static bool _gameIsBulletTime = false;

    private float prevTimeScale = 0f;
    private float prevDeltaTime = 0f;
    [SerializeField] private float slowDownFactor = 0.5f;
    [SerializeField] private float slowDownLength = 3f;
    

    public UnityEvent OnTimeRestore;

    private void Awake()
    {
        Instance = this;
    }

    public bool GameIsPaused()
    {
        return _gameIsPaused;
    }

    public void Pause()
    {
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = prevTimeScale;
        prevTimeScale = 0f;
        _gameIsPaused = false;
    }

    public void Update()
    {
        if (_gameIsBulletTime)
        {
            // restore time
            Time.timeScale += (1f / slowDownLength) * Time.deltaTime; // 3 dialated time seconds
            if (Time.timeScale >= 1)
            {
                Debug.Log("TImeRestore");
                Time.fixedDeltaTime = prevDeltaTime;
                Time.timeScale = 1f;
                _gameIsBulletTime = false;
                OnTimeRestore.Invoke();
            }
        }
    }

    public void BulletTime()
    {
        if (_gameIsBulletTime) return;
        _gameIsBulletTime = true;
        Time.timeScale = slowDownFactor;
        prevDeltaTime = Time.fixedDeltaTime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}