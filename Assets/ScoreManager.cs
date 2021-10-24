using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    [SerializeField] TextMeshProUGUI scoreText;

    private int mScore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = mScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainPoint(int points)
    {
        mScore += points;
        scoreText.text = mScore.ToString();
    }
}
