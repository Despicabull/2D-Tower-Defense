using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace UI
{
    public class SubjectsViewSubjectCard : MonoBehaviour, IPointerDownHandler 
    {
        [SerializeField] private TextMeshProUGUI nameInfoText;
        [SerializeField] private Image icon;
        [SerializeField] private Image foreground;

        [SerializeField] private Color baseColor;
        [SerializeField] private Color assignedColor;

        private string id;
        private bool isAssigned;

        public void OnPointerDown(PointerEventData eventData)
        {
            isAssigned = !isAssigned;
            Player.Instance.playerData.Subjects[id] = isAssigned;
            Player.Instance.playerData.Subjects = Player.Instance.playerData.Subjects;
            Assign();
        }

        public void Initialize(string id, bool isAssigned)
        {
            SubjectDataSO subjectDataSO = SubjectManager.Instance.GetSubjectDataSO(id);
            icon.sprite = subjectDataSO.icon;
            nameInfoText.text = $"{subjectDataSO.name}";
            this.id = id;
            this.isAssigned = isAssigned;

            Assign();
        }

        void Assign()
        {
            if (isAssigned)
            {
                // Set to assigned color
                foreground.color = assignedColor;
            }
            else
            {
                // Return to base color
                foreground.color = baseColor;
            }
        }
    }
}
