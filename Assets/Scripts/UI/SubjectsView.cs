using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class SubjectsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private GameObject subjectCardPrefab;

        private Dictionary<string, bool> subjects;

        void OnEnable()
        {
            // Destroy all current SubjectCard
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            // Initialize SquadView SubjectData
            // TODO: Change this (copy dictionary using LINQ)
            subjects = Player.Instance.playerData.Subjects.ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (KeyValuePair<string, bool> kvp in subjects)
            {
                // Instantiate SubjectCard prefab as SquadView Game Object child based on PlayerData squad
                SubjectsViewSubjectCard subjectCard = Instantiate(
                    original: subjectCardPrefab,
                    parent: content
                ).GetComponent<SubjectsViewSubjectCard>();
                subjectCard.Initialize(kvp.Key, kvp.Value);
            }
        }
    }
}

