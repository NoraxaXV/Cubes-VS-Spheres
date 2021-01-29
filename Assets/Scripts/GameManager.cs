using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int maxPlayers;
    [Range(1, 2)] public int playerNumber = 1;
    [SerializeField] int startingLayerNumber = 14;
    [SerializeField] LayerMask[] playerCamCullingMasks = { };
    [SerializeField] GameObject[] prefabs = { };
    

    // Start is called before the first frame update
    void Start()
    {
        var prefabToSpawn = prefabs[Mathf.Clamp(playerNumber, 0, prefabs.Length - 1)];
        var cameraMask = playerCamCullingMasks[Mathf.Clamp(playerNumber, 0, playerCamCullingMasks.Length - 1)];
        
        var playerGameObject = Instantiate(prefabToSpawn);
        foreach(MeshRenderer mesh in playerGameObject.GetComponentsInChildren<MeshRenderer>()) 
        {
            mesh.gameObject.layer = startingLayerNumber + playerNumber;
        }
        playerGameObject.GetComponentInChildren<Camera>().cullingMask = cameraMask;
    }

}
