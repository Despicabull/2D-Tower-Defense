using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntityDataSO : ScriptableObject
{
        [Header("General")]
        public string id; // ID template for SubjectDataSO --> Role (A, D, S) + number sequence (1 -> n)
        
        [Header("Attack")]
        public int attack;
        public int attackRange;
        public float attackRate;

        [Header("Defense")]
        public int defense;
        public int maxHealth;

        [Header("Movement")]
        public int moveSpeed;
}
