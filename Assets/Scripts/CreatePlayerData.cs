using UnityEngine;
using System;

[Serializable]
public class PlayerMesh
{
    public bool isBlenderMesh = true;
    public GameObject prefab = null;
}

public class CreatePlayerData : MonoBehaviour
{
    [SerializeField] int maxPlayers = 2;
    [Range(1, 2)] public int playerNumber = 1;
    public Camera cam;

    [SerializeField] Transform meshParent = null;

    [SerializeField] PlayerMesh[] meshPrefabs = { };

    // Start is called before the first frame update
    void Start()
    {
        if (CheckPlayerNumber(playerNumber, maxPlayers))
        {
            Debug.LogError("playerNumber is too large! Please set to between 1 and " + maxPlayers);
            return;
        }

        var go = CreateMesh(playerNumber);
        SetLayer(playerNumber);
        SetWeaponCamera(go);
    }

    private void SetWeaponCamera(GameObject go)
    {
        var weaponsManager = go.GetComponent<PlayerWeaponsManager>();
        if (!weaponsManager) return;

        weaponsManager.playerCamera = cam;
        weaponsManager.weaponCamera = cam;
    }

    public bool CheckPlayerNumber(int number, int max)
    {
        return number < 1 || number > max;
    }
    public void SetLayer(int number)
    {
        if (CheckPlayerNumber(number, maxPlayers))
        {
            Debug.LogError("number for setLayer is too large! Please set to between 1 and " + maxPlayers);
            return;
        }
        int layer = 10 + number;
        foreach (MeshRenderer mesh in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.gameObject.layer = layer;
        }
        cam.cullingMask = cam.cullingMask - (1 << layer);
    }

    public GameObject CreateMesh(int number)
    {
        if (CheckPlayerNumber(number, maxPlayers))
        {
            Debug.LogError("playerNumber is too large! Please set to between 1 and " + maxPlayers);
            return null;
        }
        var player = meshPrefabs[number - 1];
        
        var meshGo = Instantiate(player.prefab, meshParent);
        if (player.isBlenderMesh) meshGo.transform.rotation = Quaternion.Euler(0, 90, 0);
        return meshGo;
    }
}
