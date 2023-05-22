using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class HealthBar : ProgressBar
{
    [SerializeField] private LivingEntity entity;

    protected override void OnValidate()
    {
        base.OnValidate();
        entity = transform.root.GetComponent<LivingEntity>();
    }

    public void Initialize()
    {
        MaximumBar = entity.maxHealth;
        CurrentBar = entity.CurrentHealth;
        progressBarRate = 0f;
    }

    public void UpdateHealthBar(float health)
    {
        CurrentBar = health;
    }
}
