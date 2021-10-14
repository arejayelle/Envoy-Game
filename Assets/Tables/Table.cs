using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    
    private bool isDirty = false;

    [SerializeField] private Animator anim;
    public GameObject bloodEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDirty", isDirty);
    }

    // Update is called once per frame
    void Update()
    {
        anim.ResetTrigger("justCleaned");
    }

    public void infect()
    {
        isDirty = true;
        anim.SetBool("isDirty", isDirty);

    }
    public void cleanTable()
    {
        Debug.Log(isDirty? "Dirty!!!": "Already clean!");

        if (isDirty)
        {
            isDirty = false;
            anim.SetBool("isDirty", isDirty);
            anim.SetTrigger("justCleaned");
        }
        
    }
}
