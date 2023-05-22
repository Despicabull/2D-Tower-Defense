using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBar : ProgressBar
{
    // Start is called before the first frame update
    void Start()
    {
        MaximumBar = 1f;
        CurrentBar = 0f;
        progressBarRate = 0.4f;
    }
}
