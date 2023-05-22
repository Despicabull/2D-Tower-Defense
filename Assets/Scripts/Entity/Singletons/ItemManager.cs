using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [Header("Database")]
    [SerializeField] private List<ItemSO> itemScriptableObjects;

    public ItemSO GetItemSO(string id)
    {
        return itemScriptableObjects.Find(itemScriptableObject => itemScriptableObject.id == id);
    }

    public List<ItemSO> GetAllItemSO()
    {
        return itemScriptableObjects;
    }
}
