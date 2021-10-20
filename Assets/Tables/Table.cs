using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Table : MonoBehaviour, IWipeable
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
            anim.ResetTrigger("justCleaned");
            anim.SetBool("isDirty", mIsDirty);

        }

    }

    public void infect()
    {
        mIsDirty = true;
        anim.SetBool("isDirty", mIsDirty);

    }
    public void Wipe()
    {
        if (mIsDirty)
        {
            justCleaned = true;
            anim.SetTrigger("justCleaned");
        }
        
    }
}
