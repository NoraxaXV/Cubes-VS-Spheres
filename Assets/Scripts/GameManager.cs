using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int maxPlayers;
    [Range(1, 2)] public int playerNumber = 1;

    [SerializeField] GameObject[] prefabs = { };
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefabs[Mathf.Clamp(playerNumber, 0, prefabs.Length - 1)]);
    }

}
