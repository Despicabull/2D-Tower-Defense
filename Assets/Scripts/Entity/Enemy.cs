using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class Enemy : LivingEntity
    {
        [Header("Scriptable Object")]
        public EnemyDataSO enemyDataSO;

        [Header("Enemy")]
        private Action onReachedEndPath;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            StartCoroutine(FindNearestSubject());
            StartCoroutine(MoveAlongPath());
        }

        public void Initialize(EnemyDataSO enemyDataSO)
        {
            base.Initialize(enemyDataSO);

            onDeath += () =>
            {
                Player.Instance.playerData.Plasmids += enemyDataSO.maxHealth;

                Destroy(gameObject);
            };

            onReachedEndPath += () =>
            {
                // Reduce player health
                Health.Instance.HealthCount--;
                Destroy(gameObject);
            };
        }

        IEnumerator FindNearestSubject()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.2f);

                Subject[] subjects = FindObjectsOfType<Subject>();
                float shortestDistance = Mathf.Infinity;
                Subject nearestSubject = null;

                foreach (Subject subject in subjects)
                {
                    float distance = Vector2.Distance(transform.position, subject.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSubject = subject;
                    }
                }

                if (nearestSubject && pathfinding.InRange(node, nearestSubject.node, attackRange))
                {
                    attackTarget = nearestSubject.transform;
                    isAttacking = true;
                    ChangeAnimationState(AnimationState.Attack);
                }
                else
                {
                    attackTarget = null;
                    isAttacking = false;
                    ChangeAnimationState(AnimationState.Move);
                }
            }
        }

        IEnumerator MoveAlongPath()
        {
            for (int i = 0; i < movePath.Count; i++)
            {
                moveTarget = movePath[i].transform;

                // Move towards the target position while the game object is not at the tile tolerance
                while (Vector2.Distance(transform.position, moveTarget.position) > 0.1f)
                {
                    // Moves if Enemy is not attacking
                    if (!isAttacking)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
                    }

                    // Wait until next frame to continue the loop
                    yield return null;
                }
            }

            onReachedEndPath?.Invoke();
        }
    }   
}
