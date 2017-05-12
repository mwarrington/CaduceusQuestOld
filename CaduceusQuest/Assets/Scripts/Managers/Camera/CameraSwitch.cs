using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private CameraManager _camManager;
    private Camera _myCamera;

    private void Start()
    {
        _camManager = FindObjectOfType<CameraManager>();
        _myCamera = this.GetComponentInParent<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Simone" && this.enabled)
        {
            _camManager.ActivateCam(_myCamera);
        }
    }
}