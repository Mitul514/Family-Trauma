using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAssigner : MonoBehaviour
{
    private void Awake()
    {
        AssignCamera();
    }

    public void AssignCamera()
    {
        if (TryGetComponent(out Canvas canvas))
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
