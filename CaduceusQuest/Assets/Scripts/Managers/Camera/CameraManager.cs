using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera CurrentCamera
    {
        get
        {
            return _currentCamera;
        }
        set
        {
            _currentCamera = value;
        }
    }
    private Camera _currentCamera;

    private List<Camera> _allCameras = new List<Camera>();
    private Dictionary<int, CameraSwitch> _allSwitches = new Dictionary<int, CameraSwitch>();

    private void Start()
    {
        _allCameras.AddRange(FindObjectsOfType<Camera>());

        for (int i = 0; i < _allCameras.Count; i++)
        {
            CameraSwitch[] currentCamSwitches = _allCameras[i].GetComponentsInChildren<CameraSwitch>();

            for (int j = 0; j < currentCamSwitches.Length; j++)
            {
                _allSwitches.Add(i, currentCamSwitches[j]);
            }
        }
    }
}