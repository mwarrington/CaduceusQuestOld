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
            if(_currentCamera != value)
            {
                _currentCamera.enabled = false;
                _currentCamera.GetComponent<AudioListener>().enabled = false;

                _currentCamera = value;
            }
        }
    }
    private Camera _currentCamera;

    private List<Camera> _allCameras = new List<Camera>();
    private List<List<CameraSwitch>> _allSwitches = new List<List<CameraSwitch>>();
    private int _currentCamIndex;

    private void Start()
    {
        _allCameras.AddRange(FindObjectsOfType<Camera>());
        _currentCamera = Camera.main;

        for (int i = 0; i < _allCameras.Count; i++)
        {
            List<CameraSwitch> currentCamSwitches = new List<CameraSwitch>();
            currentCamSwitches.AddRange(_allCameras[i].GetComponentsInChildren<CameraSwitch>());

            _allSwitches.Add(currentCamSwitches);
        }
    }

    public void ActivateCam(Camera camToActivate)
    {
        for (int i = 0; i < _allCameras.Count; i++)
        {
            if (_allCameras[i] == camToActivate)
            {
                _currentCamIndex = i;
                camToActivate.enabled = true;
                camToActivate.GetComponent<AudioListener>().enabled = true;

                CurrentCamera = camToActivate;

                for (int j = 0; j < _allSwitches[i].Count; j++)
                {
                    _allSwitches[i][j].enabled = false;
                }
            }
            else
            {
                for (int j = 0; j < _allSwitches[i].Count; j++)
                {
                    _allSwitches[i][j].enabled = true;
                }
            }
        }
    }
}