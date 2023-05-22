using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Entity;
using UI;

public class BuildManager : Singleton<BuildManager>
{
    [Serializable]
    private class SubjectBuild
    {
        public string id;
        public GameObject prefab;
    }

    private const int MAX_SUBJECT_BUILD = 10; // The maximum amount of subject can be built

    [SerializeField] private List<SubjectBuild> subjectBuilds;
    [SerializeField] private TextMeshProUGUI subjectCounterText;

    [SerializeField] private Transform subjectCardView;
    [SerializeField] private GameObject subjectCardPrefab;
    public GameObject selectedSubject;

    private Dictionary<string, GameObject> subjectDictionary;
    private HashSet<string> squadSubjectData;
    private Subject[] subjects;

    void OnValidate()
    {
        for (int i = 0; i < subjectBuilds.Count; i++)
        {
            subjectBuilds[i].id = subjectBuilds[i].prefab?.GetComponent<Subject>().subjectDataSO.id;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        subjectDictionary = new Dictionary<string, GameObject>();

        foreach (SubjectBuild subjectBuild in subjectBuilds)
        {
            subjectDictionary.Add(subjectBuild.id, subjectBuild.prefab);
        }

        // Initialize Shop SubjectData
        squadSubjectData = new HashSet<string>(Player.Instance.playerData.Subjects.Where(pair => pair.Value).Select(pair => pair.Key));

        foreach (string s in squadSubjectData)
        {
            // Instantiate SubjectCard prefab as SquadView Game Object child based on PlayerData squad
            ShopViewSubjectCard subjectCard = Instantiate(
                original: subjectCardPrefab,
                parent: subjectCardView
            ).GetComponent<ShopViewSubjectCard>();
            subjectCard.Initialize(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindAllSubjects();
    }

    public bool ValidateBuild(SubjectDataSO subjectDataSO)
    {
        if (!Credit.Instance.SufficientCredit(subjectDataSO.creditCost))
        {
            Debug.LogError("Insufficient Credit");
            return false;
        }

        if (subjects.Length >= MAX_SUBJECT_BUILD)
        {
            Debug.LogError("Overloaded Subject");
            return false;
        }

        return true;
    }
    
    public void Build(SubjectDataSO subjectDataSO, Node node)
    {
        // Take subject from object pool and initialize subject data based on subjectData
        Subject subject = Instantiate(
            original: GetSubject(subjectDataSO.id),
            position: transform.position,
            rotation: transform.rotation,
            parent: transform).GetComponent<Subject>();
        subject.Initialize(subjectDataSO);
        subject.Spawn(node.x, node.y);

        Credit.Instance.DecreaseCreditCount(subjectDataSO.creditCost);

        node.isOccupied = true;
    }

    void FindAllSubjects()
    {
        subjects = FindObjectsOfType<Subject>();

        subjectCounterText.text = $"{subjects.Length.ToString()} / {MAX_SUBJECT_BUILD.ToString()}";
    }

    GameObject GetSubject(string id)
    {
        return subjectDictionary[id];
    }
}
