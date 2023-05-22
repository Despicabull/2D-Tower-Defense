using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public abstract class LivingEntity : EntityClass
    {
        protected enum AnimationState
        {
            Idle,
            Move,
            Attack,
            Death
        }

        protected enum Order
        {
            Attack,
            Move
        }

        private AnimationState currentState;

        [Header("General")]
        public string id;
        
        [Header("Attack")]
        public int attack;
        public int attackRange;
        public float attackRate;
        protected bool isAttacking;
        protected Action<int> onAttack;
        protected float attackCountdown = 0f;
        protected Transform attackTarget;

        [Header("Defense")]
        public int defense;
        private float currentHealth;
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                currentHealth = value;
                healthBar.UpdateHealthBar(currentHealth);
            }
        }
        public float maxHealth;
        public Action<float> onHealthChanged;
        public Action onDeath;
        protected Action<int> onAttacked;

        [Header("Movement")]
        public float moveSpeed;
        protected Transform moveTarget;

        [Header("Components")]
        [SerializeField] private HealthBar healthBar;

        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            if (attackTarget == null) { return; }

            if (attackCountdown <= 0f)
            {
                Attack(attackTarget);
                attackCountdown = 1f / attackRate;
            }

            attackCountdown -= Time.deltaTime;
        }

        protected void Initialize(LivingEntityDataSO livingEntityDataSO)
        {
            attack = livingEntityDataSO.attack;
            attackRange = livingEntityDataSO.attackRange;
            attackRate = livingEntityDataSO.attackRate;

            defense = livingEntityDataSO.defense;

            moveSpeed = livingEntityDataSO.moveSpeed;

            maxHealth = livingEntityDataSO.maxHealth;
            healthBar.Initialize();
            CurrentHealth = maxHealth;

            onAttacked += TakeDamage;
            onDeath += () =>
            {
                ChangeAnimationState(AnimationState.Death);
            };
        }

        protected virtual void Attack(Transform target)
        {
            if (target.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (target.position.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            // Invoke onAttack on attacker and onAttacked on attacked
            onAttack?.Invoke(attack);
            target?.GetComponent<LivingEntity>().onAttacked?.Invoke(attack);
        }

        protected bool InRange(Node startNode, Node endNode)
        {
            int distanceX = Mathf.Abs(startNode.x - endNode.x);
            int distanceY = Mathf.Abs(startNode.y - endNode.y);

            return distanceX <= attackRange && distanceY <= attackRange;
        }

        protected void ChangeAnimationState(AnimationState newState)
        {
            if (currentState == newState) { return; }

            animator.Play(newState.ToString());

            currentState = newState;
        }

        void TakeDamage(int amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0f, maxHealth);

            if (CurrentHealth <= 0f)
            {
                // Entity dies
                onDeath?.Invoke();
            }
        }
    }
}
