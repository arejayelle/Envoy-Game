using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
    public static GameAssetManager i;

    public Transform popupText;

    private void Awake()
    {
        i = this;
    }
}
