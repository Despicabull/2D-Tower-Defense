using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StoreViewItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image icon;

    private int plasmidCost;
    private int ticketCount;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.playerData.Plasmids >= plasmidCost)
        {
            Player.Instance.playerData.Tickets += ticketCount;
            Player.Instance.playerData.Plasmids -= plasmidCost;
        }
        else
        {
            ModalWindowManager.Instance.SetTitleAndMessage("Insufficient Plasmid", "Insufficient Plasmid");
            ModalWindowManager.Instance.ShowModalWindow();
        }
    }

    public void Initialize(ItemSO itemSO)
    {
        infoText.text = $"{itemSO.name}\n{itemSO.plasmidCost}";
        icon.sprite = itemSO.icon;

        plasmidCost = itemSO.plasmidCost;
        ticketCount = itemSO.ticketCount;
    }
}
