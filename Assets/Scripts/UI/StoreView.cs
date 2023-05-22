using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreView : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemPrefab;

    private Dictionary<string, int> items;

    void OnEnable()
    {
        // Destroy all current Item
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        
        foreach (ItemSO itemSO in ItemManager.Instance.GetAllItemSO())
        {
            StoreViewItem storeViewItem = Instantiate(
                original: itemPrefab,
                parent: content
            ).GetComponent<StoreViewItem>();
            storeViewItem.Initialize(itemSO);
        }
    }
}
