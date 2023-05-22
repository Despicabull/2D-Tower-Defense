using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalWindowManager : Singleton<ModalWindowManager>
{
    [SerializeField] private ModalWindow modalWindow;

    // Set the title and message text for the modal window
    public void SetTitleAndMessage(string title, string message)
    {
        modalWindow.titleText.text = title;
        modalWindow.messageText.text = message;
    }

    // Show the modal window and attach button listeners
    public void ShowModalWindow()
    {
        modalWindow.gameObject.SetActive(true);

        modalWindow.confirmButton.onClick.AddListener(ConfirmButtonListener);
    }

    // Hide the modal window and remove button listeners
    public void HideModalWindow()
    {
        modalWindow.gameObject.SetActive(false);

        modalWindow.confirmButton.onClick.RemoveListener(ConfirmButtonListener);
    }

    // This method is called when the Confirm button is clicked
    private void ConfirmButtonListener()
    {
        // Add your code here for when the Confirm button is clicked
        // For example, you might save the user's settings or progress
        HideModalWindow();
    }
}
