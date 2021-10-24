using System;
using UnityEngine;

[Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float spawnRate;
    }
