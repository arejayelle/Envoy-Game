using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTimeManager : MonoBehaviour
{
    public static BulletTimeManager instance;
    private BulletTimeSpawner bulletTimeSpawner;
    public WaveSpawner RegularSpawner;
    [SerializeField] PlayerController player;
    public bool mCanBulletTime = true;

    [Header("Display")] 
    private String prevRoundText = "";
    [SerializeField] TextMeshProUGUI RoundText;
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletTimeSpawner = transform.GetComponent<BulletTimeSpawner>();
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
        StartCoroutine(RoundManager.Instance.textFlash());
        Invoke("OnBulletTimeEnd", 10f);
        return true;
    }
    
    public void OnBulletTimeEnd()
    {
        // Reset
        TimeManager.Instance.BulletTimeEnd();
        player.bulletTimeEnd();
        bulletTimeSpawner.BulletTimeEnd();
        
        // Flip Spawners
        bulletTimeSpawner.enabled = false;
        RegularSpawner.enabled = true;
        
        // Change Round Text
        RoundText.text = prevRoundText;
        
        // can bullet time again in 30 seconds
        Invoke("BulletTimeReset", 30f);
    }

    void BulletTimeReset()
    {
        mCanBulletTime = true;
    }

}
