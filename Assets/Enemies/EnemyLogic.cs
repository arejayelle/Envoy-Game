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

        [SerializeField] private bool isKickedOut = false;
        
        protected virtual void Start()
        {
            mask.SetActive(isMasked);
            if(isMasked) MaskUp();
            if(isInfected) HandleInfection();
        }

        private void Update()
        {
            if (isInfected)
            {
                InfectOthers();
            }
        }
        // Infection behaviours
        protected override void HandleInfection()
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

            transform.GetComponent<EnemyMovement>().enabled = false;
            transform.GetComponent<Collider2D>().enabled = false;
            isDead = true;
            Invoke("RemoveEvidence", 0.5f);

        }

        void RemoveEvidence()
        {
            Destroy(gameObject);
        }

// mask behaviours
        public bool MaskUp()
        {
            if (isMasked || isDead) return false;
            
            isMasked = true;
            mask.SetActive(true);
            immunity += maskEffect;
            infectionBonus = 5;
            infectionRange = 0.5f;
            Debug.Log("Masking!");
            
            return true;

        }

        public void Despawn()
        {
            Destroy(gameObject);
        }
        
    }