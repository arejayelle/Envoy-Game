using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLogic : Infectable
    {
        public bool isMasked;
        public GameObject mask;
        public int maskEffect = 20;
        
        public SpriteRenderer bodySpriteRenderer;

        public bool isImmunocompromised = false;
        
        protected virtual void Start()
        {
            mask.SetActive(false);
            if(isInfected) handleInfection();
        }

        private void Update()
        {
            if (isInfected)
            {
                infectOthers();
            }
        }
        // Infection behaviours
        protected override void handleInfection()
        {
            if (isImmunocompromised)
            {
                die();
                return;
            }
                
            bodySpriteRenderer.color = Color.green;
        }

        private void die()
        {
            bodySpriteRenderer.color = Color.black;
            // disable script
        }

// mask behaviours
        public bool maskUp()
        {
            if (!isMasked)
            {
                isMasked = true;
                mask.SetActive(true);
                immunity += maskEffect;
                infectionStrength = 5;
                Debug.Log("Masking!");
                return true;
            }

            Debug.Log("Already masked");
            return false;
        }
        
    }