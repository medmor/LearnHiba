using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCulling : MonoBehaviour
{
    public LayerMask layer;
    void Start()
    {
        var cam = GetComponent<Camera>();
        cam.cullingMask = layer;
        Camera.main.cullingMask = ~(1 << 8);
    }

}
