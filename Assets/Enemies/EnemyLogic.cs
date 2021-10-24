using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLogic : Infectable
    {
        [Header("Masks")]
        [SerializeField] bool isMasked;
        [SerializeField] GameObject mask;
        [SerializeField] int maskEffect = 20;
        
        [Header("General")]
        [SerializeField] SpriteRenderer bodySpriteRenderer;
        [SerializeField] bool isDead = false;
        [SerializeField] bool isImmunocompromised = false;
        
        protected virtual void Start()
        {
            mask.SetActive(isMasked);
            if(isMasked) maskUp();
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
        public bool MaskUp()
        {
            if (isMasked || isDead) return false;
            
            isMasked = true;
            mask.SetActive(true);
            immunity += maskEffect;
            infectionStrength = 5;
            infectionRange = 0.5f;
            Debug.Log("Masking!");
            
            return true;

        }
        
    }