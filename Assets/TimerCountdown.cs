using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float RoundTime = 15f;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = RoundTime;

        StartCoroutine(TimerTake());
    }
    
    IEnumerator TimerTake()
    {
        do
        {
            timer -= Time.deltaTime;
            FormatText();
            yield return null;
        } while (timer>0);
    }

    void FormatText()
    {
        var minutes = (int) (timer / 60) % 60;
        var seconds = timer % 60;
        timerText.text = $"{minutes}:{seconds:F2}";

    }
}
