using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private Transform UpdateText;

    private void Start()
    {
        PopupText.SetPosition(UpdateText.transform);
    }
    
}
