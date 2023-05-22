using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SubjectData", menuName = "ScriptableObjects/SubjectData")]
public class SubjectDataSO : LivingEntityDataSO
{
    public enum Role
    {
        Attack,
        Defense,
        Support
    }

    public enum Rarity
    {
        N,
        R,
        SR,
        SSR
    }

    [Header("Subject")]
    public Sprite sprite;
    public Sprite icon;
    public Role role;
    public Rarity rarity;
    public int creditCost;
    public float energyChargeRate;
    public float buildCooldown;
}
