using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credit : Singleton<Credit>
{
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private CreditBar creditBar;

    private int creditCount;
    private readonly int maximumCreditCount = 100;

    // Start is called before the first frame update
    void Start()
    {
        creditCount = 10;

        creditText.text = creditCount.ToString();

        creditBar.barFilled += IncreaseCreditCount;
        creditBar.barFilled += creditBar.ResetBar;
    }

    public bool SufficientCredit(int amount)
    {
        return creditCount >= amount;
    }

    public void IncreaseCreditCount()
    {
        creditCount = Mathf.Min(++creditCount, maximumCreditCount);
        creditText.text = creditCount.ToString();
    }

    public void DecreaseCreditCount(int amount)
    {
        creditCount = Mathf.Min(creditCount - amount, maximumCreditCount);
        creditText.text = creditCount.ToString();
    }
}
