using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Table : Infectable, IWipeable
{
    
    private bool justCleaned = false;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isInfected = true;
        anim.SetBool("isDirty", isInfected);
    }

    // Update is called once per frame
    void Update()
    {
        if (justCleaned)
        {
            isInfected = false;
            justCleaned = false;
            anim.ResetTrigger("justCleaned");
            anim.SetBool("isDirty", isInfected);

        }
        else if(isInfected)
        {
            infectOthers();
        }

    }
    
    public void Wipe()
    {
        if (isInfected)
        {
            justCleaned = true;
            anim.SetTrigger("justCleaned");
        }
        
    }

    protected override void handleInfection()
    {
        anim.SetBool("isDirty", isInfected);
    }
}
