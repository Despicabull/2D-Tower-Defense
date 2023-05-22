using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TabGroup tabGroup;

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        void OnValidate()
        {
            tabGroup = transform.parent.GetComponent<TabGroup>();
        }

        // Start is called before the first frame update
        void Start()
        {
            tabGroup.Subscribe(this);
        }
    }
}
