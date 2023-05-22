using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : Singleton<MenuManager>
{

    [Header("Top")]
    [SerializeField] private TextMeshProUGUI plasmidInfoText;
    [SerializeField] private TextMeshProUGUI ticketInfoText;

    [Header("Combat")]
    [SerializeField] private Button startGameButton;

    [Header("Hyperconstruct")]
    [SerializeField] private Button hyperconstructButton;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        hyperconstructButton.onClick.AddListener(Hyperconstruct);
    }

    // Update is called once per frame
    void Update()
    {
        plasmidInfoText.text = Player.Instance.playerData.Plasmids.ToString();
        ticketInfoText.text = Player.Instance.playerData.Tickets.ToString();
    }

    void StartGame()
    {
        if (Player.Instance.playerData.Subjects.Any(pair => pair.Value == true))
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            ModalWindowManager.Instance.SetTitleAndMessage("No Subject", "Atleast 1 Subject is needed to Start");
        }
    }

    void Hyperconstruct()
    {
        if (Player.Instance.playerData.Tickets > 0)
        {
            SubjectDataSO rolled = (SubjectDataSO) GachaManager.Instance.Roll().item;
            Player.Instance.playerData.Subjects.Add(rolled.id, false);
            Player.Instance.playerData.Subjects = Player.Instance.playerData.Subjects;

            Player.Instance.playerData.Tickets--;

            ModalWindowManager.Instance.SetTitleAndMessage("Hyperconstruct Completed", $"You received {rolled.name}");
        }
        else
        {
            ModalWindowManager.Instance.SetTitleAndMessage("Insufficient Ticket", "Insufficient Ticket");
        }

        ModalWindowManager.Instance.ShowModalWindow();
    }
}
