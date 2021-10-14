using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Table : MonoBehaviour
{
    
    private bool mIsDirty = true;
    private bool justCleaned = false;

    private Animator anim;
    public GameObject bloodEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDirty", mIsDirty);
    }

    // Update is called once per frame
    void Update()
    {
        if (justCleaned)
        {
            mIsDirty = false;
            justCleaned = false;
        }

        if (!mIsDirty)
        {
            var randomPoint = Random.value;
            if (randomPoint > 0.1f)
            {
                Debug.Log("infected");
                infect();
            }
        }
        anim.SetBool("isDirty", mIsDirty);

    }

    public void infect()
    {
        mIsDirty = true;
        anim.SetBool("isDirty", mIsDirty);

    }
    public void CleanTable()
    {
        Debug.Log("BEEP");
        Debug.Log(mIsDirty? "Dirty!!!": "Already clean!");

        if (mIsDirty)
        {
            justCleaned = true;
            anim.SetTrigger("justCleaned");
        }
        
    }
}
