using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressBar : MonoBehaviour
{
    [SerializeField] private Transform background;
    [SerializeField] private Transform foreground;

    public Action barFilled;
    public Action barEmpty;

    private float maximumBar;
    private float currentBar;

    protected virtual float MaximumBar { get => maximumBar; set => maximumBar = value; }
    protected virtual float CurrentBar
    {
        get => currentBar;
        set
        {
            currentBar = Mathf.Clamp(value, 0f, MaximumBar);
            
            if (currentBar >= MaximumBar) { barFilled?.Invoke(); }

            GetCurrentFill();
        }
    }

    protected float progressBarRate;

    protected virtual void OnValidate()
    {
        background = transform.Find("Background");
        foreground = transform.Find("Foreground");
    }

    // Update is called once per frame
    void Update()
    {
        CurrentBar += (progressBarRate * Time.deltaTime);
    }

    public void ResetBar()
    {
        CurrentBar = 0f;
    }

    public void GetCurrentFill()
    {
        float fillAmount = (float)CurrentBar / MaximumBar;
        foreground.localScale = new Vector3(fillAmount, transform.localScale.y, transform.localScale.z);
    }
}
