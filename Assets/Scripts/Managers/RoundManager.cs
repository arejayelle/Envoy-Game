using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    private static RoundManager instance;
    
    [SerializeField] TextMeshProUGUI RoundText;

    [SerializeField] int mRoundNumber = 1;
    
    public int RoundNumber => mRoundNumber;

    public static RoundManager Instance => instance;

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
        RoundText.enabled = false;
        RoundText.text = $"Round {mRoundNumber}";
        StartCoroutine(textFlash());
    }

    public IEnumerator textFlash()
    {
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = true;
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = false;
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = true;
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = false;
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = true;
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = false;
        yield return new WaitForSeconds(.3f);
        RoundText.enabled = true;
        yield break;
    }
}
