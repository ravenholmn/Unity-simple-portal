using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraA;
    public Material cameraMatA;
    public Camera cameraB;
    public Material cameraMatB;

    void Start()
    {
        SetTexture(cameraA, cameraMatA);
        SetTexture(cameraB, cameraMatB);
    }

    void SetTexture(Camera camera, Material material)
    {
        if (camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }

        camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        material.mainTexture = camera.targetTexture;
    }
}
