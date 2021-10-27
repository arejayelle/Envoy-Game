using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTimeManager : MonoBehaviour
{
    private WaveSpawner bulletTimeSpawner;
    public WaveSpawner RegularSpawner;
    [SerializeField] PlayerController player;
    public bool mCanBulletTime = true;

    [Header("Display")] 
    private String prevRoundText = "";
    [SerializeField] TextMeshProUGUI RoundText;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletTimeSpawner = transform.GetComponent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DoBulletTime()
    {
        if (!mCanBulletTime)
        {
            Debug.Log("No more bullets");
            return false;
        }
        
        Debug.Log("BulletTime");
        TimeManager.Instance.BulletTime();
        mCanBulletTime = false;
        // Switch spawners
        bulletTimeSpawner.enabled = true;
        RegularSpawner.enabled = false;
        
        // Change Round Text
        prevRoundText = RoundText.text;
        RoundText.text = "BULLET TIME!";
        return true;
    }
    
    public void OnBulletTimeEnd()
    {
        bulletTimeSpawner.enabled = false;
        RegularSpawner.enabled = true;
        RoundText.text = prevRoundText;
    }

}
