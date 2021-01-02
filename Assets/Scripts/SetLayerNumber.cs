using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayerNumber : MonoBehaviour
{
    [Range(1, 2)] public int playerNumber = 1;
    public Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        SetLayer(playerNumber);
    }
    void SetLayer(int number) 
    {
        gameObject.layer = 10 + number;
        camera.cullingMask = camera.cullingMask - (1 >> gameObject.layer);
    }
}
