using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Color clickColor;
        [SerializeField] private Color unclickColor;
        [SerializeField] private List<GameObject> objectsToSwap;

        private List<TabButton> tabButtons;
        private TabButton selectedTab;

        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }

            tabButtons.Add(button);
        }

        public void OnTabSelected(TabButton button)
        {
            ResetTabs();
            selectedTab = button;
            button.GetComponent<Image>().color = clickColor;
            int index = button.transform.GetSiblingIndex();

            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                    break;
                }
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                // Change the color
                button.GetComponent<Image>().color = unclickColor;
            }

            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(false);
            }

            selectedTab = null;
        }
    }
}
