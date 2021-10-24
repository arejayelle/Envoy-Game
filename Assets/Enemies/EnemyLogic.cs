using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLogic : Infectable
    {
        public bool isMasked;
        public bool isDead = false;
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
                Die();
                return;
            }
                
            bodySpriteRenderer.color = Color.green;
        }

        private void Die()
        {
            bodySpriteRenderer.color = Color.black;
            
            // disable script

            transform.GetComponent<EnemyMovement>().Die();
            transform.GetComponent<Collider2D>().enabled = false;
            isDead = true;
        }

// mask behaviours
        public void maskUp()
        {
            if (isMasked || isDead) return;
            
            isMasked = true;
            mask.SetActive(true);
            immunity += maskEffect;
            infectionStrength = 5;
            infectionRange = 0.5f;
            Debug.Log("Masking!");
            return;

        }
        
    }