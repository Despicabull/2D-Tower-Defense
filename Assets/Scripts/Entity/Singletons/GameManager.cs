using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        LoadingManager.Instance.LoadScene((int)SceneIndices.Main);
    }

    public void ReturnToMainMenu()
    {
        LoadingManager.Instance.LoadScene((int)SceneIndices.Menu);
    }
}
