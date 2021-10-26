using System;
using UnityEngine;

[Serializable]
public class Wave
{
    public string name;
    public bool isActive = true;
    public bool isMob = false;
    [Range(0, 10)] public int[] numEnemies;
}