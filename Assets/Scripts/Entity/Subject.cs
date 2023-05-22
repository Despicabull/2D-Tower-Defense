using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class Subject : LivingEntity
    {
        [Header("Subject")]
        private SubjectDataSO.Role role;
        private float energyChargeRate;

        [Header("Scriptable Object")]
        public SubjectDataSO subjectDataSO;
        [SerializeField] private EnergyBar energyBar;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            StartCoroutine(FindNearestEnemy());
        }

        public void Initialize(SubjectDataSO subjectDataSO)
        {
            base.Initialize(subjectDataSO);
            role = subjectDataSO.role;
            energyChargeRate = subjectDataSO.energyChargeRate;
            energyBar.Initialize();

            onAttack += (int damage) => energyBar.ChargeEnergy(damage, energyChargeRate);

            onDeath += () =>
            {
                node.isOccupied = false;
                Destroy(gameObject);
            };
        }

        IEnumerator FindNearestEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.2f);

                Enemy[] enemies = FindObjectsOfType<Enemy>();
                float shortestDistance = Mathf.Infinity;
                Enemy nearestEnemy = null;

                foreach (Enemy enemy in enemies)
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }

                if (nearestEnemy && pathfinding.InRange(node, nearestEnemy.node, attackRange))
                {
                    attackTarget = nearestEnemy.transform;
                    isAttacking = true;
                    ChangeAnimationState(AnimationState.Attack);
                }
                else
                {
                    attackTarget = null;
                    isAttacking = false;
                    ChangeAnimationState(AnimationState.Idle);
                }
            }
        }
    }
}
