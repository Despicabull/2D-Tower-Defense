using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Persistence;

public class Player : Singleton<Player>
{
    public PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
