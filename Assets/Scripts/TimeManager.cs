using UnityEngine;

    public class TimeManager : MonoBehaviour
    {
        private static bool _gameIsPaused;

        public static bool GameIsPaused()
        {
            return _gameIsPaused;
        }

        public static void Pause()
        {
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }
        public static void Resume()
        {
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }
    }
