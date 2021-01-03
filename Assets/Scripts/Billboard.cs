using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    public Camera cam;

    private void Awake()
    {
        if (Camera.main) cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
#if UNITY_EDITOR
            transform.LookAt(SceneView.GetAllSceneCameras()[0].transform);
#endif       
        else
            if (cam) transform.LookAt(cam.transform);

        transform.forward = -transform.forward;

    }
}
