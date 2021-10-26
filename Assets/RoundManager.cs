using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    public static RoundManager instance;
    
    [SerializeField] TextMeshProUGUI RoundText;

    private int mRoundNumber = 1;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RoundText.text  = $"Round {mRoundNumber}";
    }

    public void NewRound()
    {
        mRoundNumber++;
        RoundText.text = $"Round {mRoundNumber}";
    }
}
