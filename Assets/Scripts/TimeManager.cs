using UnityEngine;

    public class TimeManager : MonoBehaviour
    {
        public static bool GameIsPaused;

        public static void Pause()
        {
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        public static void Resume()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }
