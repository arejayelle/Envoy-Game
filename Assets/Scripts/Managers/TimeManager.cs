using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private bool mGameIsPaused;
    private bool mGameIsBulletTime = false;
    public  bool IsBulletTime => mGameIsBulletTime;

    private float mPrevTimeScale = 0f;
    private float mPrevDeltaTime = 0f;
    [SerializeField] private float slowDownFactor = 0.5f;
    [SerializeField] private float slowDownLength = 3f;
    

    // public UnityEvent OnBulletTimeEnd;

    private void Awake()
    {
        Instance = this;
    }

    public bool GameIsPaused()
    {
        return mGameIsPaused;
    }

    public void Pause()
    {
        mPrevTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        mGameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = mPrevTimeScale;
        mPrevTimeScale = 0f;
        mGameIsPaused = false;
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        mPrevTimeScale = 0f;
        mGameIsPaused = false;
        mGameIsBulletTime = false;
    }

    public void Update()
    {
    }

    public void BulletTimeEnd()
    {
        // Debug.Log("TImeRestore");
        // Time.fixedDeltaTime = prevDeltaTime;
        // Time.timeScale = 1f;
        mGameIsBulletTime = false;
        // OnBulletTimeEnd.Invoke();
    }
    public void BulletTime()
    {
        if (mGameIsBulletTime) return;
        mGameIsBulletTime = true;
        // Time.timeScale = slowDownFactor;
        // prevDeltaTime = Time.fixedDeltaTime;
        // Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}