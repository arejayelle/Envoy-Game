using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private bool isDirty = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void infect()
    {
        isDirty = true;
    }
    public void cleanTable()
    {
        Debug.Log(isDirty? "dirty!!": "clean!");
    }
}
