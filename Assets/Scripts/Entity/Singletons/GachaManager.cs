using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : Singleton<GachaManager>
{
    [Serializable]
    public class GachaItem
    {
        public ScriptableObject item; // Can be item or subject
        [Range(0f, 1f)]
        public float probability;
    }

    [SerializeField] private List<GachaItem> gachaItems;

    public GachaItem Roll()
    {
        float totalProbability = 0f;
        foreach (GachaItem gachaItem in gachaItems)
        {
            totalProbability += gachaItem.probability;
        }

        float roll = UnityEngine.Random.Range(0f, totalProbability);
        float currentProbability = 0f;

        foreach (GachaItem gachaItem in gachaItems)
        {
            currentProbability += gachaItem.probability;
            if (currentProbability >= roll)
            {
                return gachaItem;
            }
        }

        return null;
    }
}
