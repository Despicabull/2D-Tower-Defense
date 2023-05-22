using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Persistence;

public class SubjectManager : Singleton<SubjectManager>
{
    [Header("Database")]
    [SerializeField] private List<SubjectDataSO> subjectDataScriptableObjects;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public SubjectDataSO GetSubjectDataSO(string id)
    {
        return subjectDataScriptableObjects.Find(subjectDataScriptableObject => subjectDataScriptableObject.id == id);
    }
}
