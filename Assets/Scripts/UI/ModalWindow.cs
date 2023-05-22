using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModalWindow : MonoBehaviour
{
    // Declare your UI elements
    [HideInInspector] public TextMeshProUGUI titleText;
    [HideInInspector] public TextMeshProUGUI messageText;
    [HideInInspector] public Button confirmButton;
}
