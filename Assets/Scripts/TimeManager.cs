using System;
using UnityEngine;

    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;

        private static bool _gameIsPaused;
        private static bool _gameIsBulletTime = false;
        
        private float prevTimeScale = 0f;
        [SerializeField] private float slowDownFactor = 0.05f;
        [SerializeField] private float slowDownLength = 3f;
        
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
                Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
                if (Time.timeScale >= 1)
                {
                    Time.timeScale = 1f;
                    _gameIsBulletTime = false;
                }
            }
            
        }

        public void BulletTime()
        {
            _gameIsBulletTime = true;
            Time.timeScale = slowDownFactor;
            
        }
    }
