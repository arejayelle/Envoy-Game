using System;
using UnityEngine;

[Serializable]
    public class Wave
    {
        public string name;
        [Range(0,25)]
        public int[] numEnemies;
        public int count;
        public float spawnRate;
        
    }
