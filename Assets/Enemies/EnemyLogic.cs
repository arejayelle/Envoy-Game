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

        
        [Header("Movement")]
        public float speed = 3f;
        [SerializeField] float floorCheckDistance= 2f;
        public bool isMovingRight = true;
        [SerializeField] Transform GroundDetection;

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
                SpreadInfection();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        // movement
        void Move()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            // rb.velocity = new Vector2(speed , rb.velocity.y);
            RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, floorCheckDistance);
            if (groundInfo.collider == false)
            {
                Flip();
            }
        }

        public void Flip()
        {
            transform.eulerAngles = new Vector3(0, isMovingRight?-180:0, 0);
            isMovingRight = !isMovingRight;
            // transform.Rotate(0f, 180f, 0f);
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
        // dying and removal
        private void Die()
        {
            bodySpriteRenderer.color = Color.black;
            
            // disable script

            transform.GetComponent<EnemyMovement>().enabled = false;
            transform.GetComponent<Collider2D>().enabled = false;
            isDead = true;
            Invoke("Despawn", 0.5f);

        }

        public void Despawn()
        {
            Destroy(gameObject);
        }
        
    }