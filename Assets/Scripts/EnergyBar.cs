using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class EnergyBar : ProgressBar
{
    protected override void OnValidate()
    {
        base.OnValidate();
    }

    public void Initialize()
    {
        MaximumBar = 100f;
        CurrentBar = 0f;
        progressBarRate = 2f;
    }

    public void ChargeEnergy(int damage, float energyChargeRate)
    {
        CurrentBar += (damage * energyChargeRate / 10f);
    }
}