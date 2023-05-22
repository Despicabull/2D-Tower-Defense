using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("General")]
    public string id;
    public int plasmidCost;
    public int ticketCount;
    public Sprite icon;
}
