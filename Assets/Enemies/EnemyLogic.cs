using System;
using UnityEngine;
public class EnemyLogic : MonoBehaviour
    {
        public bool isMasked;
        public float infectionChance = 90f;
        private void Update()
        {
            
        }

        public bool maskUp()
        {
            if (!isMasked)
            {
                isMasked = true;
                infectionChance -= 0.5f;
                Debug.Log("Masking!");
                return true;
            }

            Debug.Log("Already masked");
            return false;
        }
    }