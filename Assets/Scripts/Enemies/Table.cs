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
        anim.SetBool("isDirty", isInfected);

    }

    // Update is called once per frame
    void Update()
    {
        if (justCleaned)
        {
            isInfected = false;
            justCleaned = false;
            anim.SetBool("isDirty", isInfected);

        }
        else if(isInfected)
        {
            SpreadInfection();
        }

    }
    
    public void Wipe()
    {
        if (isInfected)
        {
            justCleaned = true;
            GameManager.instance.tableCounter();
            ScoreManager.instance.GainPoint(ScoreType.TableWiped);
            anim.SetTrigger("justCleaned");
        }
        
    }

    protected override void HandleInfection()
    {
        ScoreManager.instance.GainPoint(ScoreType.TableInfected);
        anim.SetBool("isDirty", isInfected);
    }
}