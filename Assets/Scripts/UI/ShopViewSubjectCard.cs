using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class ShopViewSubjectCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image cooldownPanel;
        [SerializeField] private TextMeshProUGUI nameInfoText;
        [SerializeField] private TextMeshProUGUI costInfoText;

        private float cooldown;
        private Transform currentHitTransform;
        private SubjectDataSO subjectDataSO;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (cooldown <= 0f)
            {
                BuildManager.Instance.selectedSubject.GetComponent<SpriteRenderer>().sprite = subjectDataSO.sprite;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (cooldown <= 0f)
            {
                RaycastHit2D hit;

                // Display preview after deploy if drag position is on node (might change later)
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero);

                // Check if hit object is not null and is a unoccupied
                if (hit && hit.transform.GetComponent<Node>() != null && !hit.transform.GetComponent<Node>().isOccupied)
                {
                    if (currentHitTransform != hit.transform)
                    {
                        currentHitTransform = hit.transform;
                    }

                    if (BuildManager.Instance.selectedSubject.transform != hit.transform)
                    {
                        BuildManager.Instance.selectedSubject.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y);
                    }
                    
                    if (!BuildManager.Instance.selectedSubject.gameObject.activeSelf)
                    {
                        BuildManager.Instance.selectedSubject.gameObject.SetActive(true);
                    }
                }
                else
                {
                    // Unselect previous hit transform (if any)
                    currentHitTransform = null;
                    BuildManager.Instance.selectedSubject.gameObject.SetActive(false);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (cooldown <= 0f)
            {
                BuildManager.Instance.selectedSubject.gameObject.SetActive(false);

                // Unselect previous hit transform (if any)
                currentHitTransform = null;

                RaycastHit2D hit;

                // Check if touch position is in board or not
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                // Check if hit object is not null and is a unoccupied
                if (hit && hit.transform.GetComponent<Node>() != null && !hit.transform.GetComponent<Node>().isOccupied)
                {
                    if (BuildManager.Instance.ValidateBuild(subjectDataSO) && cooldown <= 0f)
                    {
                        BuildManager.Instance.Build(subjectDataSO, hit.transform.GetComponent<Node>());
                        // Place the subject card on cooldown
                        cooldown = subjectDataSO.buildCooldown;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            cooldownPanel.fillAmount = cooldown / subjectDataSO.buildCooldown;
            cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
        }

        public void Initialize(string id)
        {
            subjectDataSO = SubjectManager.Instance.GetSubjectDataSO(id);
            icon.sprite = subjectDataSO.icon;
            nameInfoText.text = $"{subjectDataSO.name}";
            costInfoText.text = $"{subjectDataSO.creditCost}";
        }
    }
}