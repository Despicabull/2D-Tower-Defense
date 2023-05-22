using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : Singleton<Health>
{
    [SerializeField] private TextMeshProUGUI healthText;

    private int healthCount;
    private readonly int maxHealthCount = 30;

    public int HealthCount
    {
        get { return healthCount; }
        set
        {
            healthCount = Mathf.Min(value, maxHealthCount);
            healthText.text = $"{healthCount} / {maxHealthCount}";

            if (healthCount <= 0)
            {
                LevelManager.Instance.GameOver();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthCount = maxHealthCount;
    }
}
