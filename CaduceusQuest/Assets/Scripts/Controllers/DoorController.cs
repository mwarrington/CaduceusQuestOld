﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Camera MoveToCamera;
    public Transform MoveToTransform;
    public string SceneToLoad;
    public float FadeTime;
    public bool DoorLocked;

    private SimoneController _theSimoneController;
    private DialogueUIController _theDialogUIController;
    private NiceSceneTransition _sceneTrans;
    private CameraManager _theCamMan;
    private SpriteRenderer _currentFadeMask,
                           _nextFadeMask;
    private GameObject _simone;
    private bool _nearDoor,
                 _fadingIn,
                 _fadingOut;

    private void Start()
    {
        _theCamMan = FindObjectOfType<CameraManager>();
        _sceneTrans = FindObjectOfType<NiceSceneTransition>();
        _currentFadeMask = _theCamMan.CurrentCamera.GetComponentInChildren<SpriteRenderer>();
        _theDialogUIController = FindObjectOfType<DialogueUIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Simone")
        {
            _nearDoor = true;
            _simone = other.gameObject;
            _theSimoneController = _simone.GetComponent<SimoneController>();
            if (DoorLocked)
                _theDialogUIController.ToggleLockedObj(true);
            else
                _theDialogUIController.ToggleInteractObj(true);
        }
    }

    private void Update()
    {
        if(_nearDoor && !DoorLocked)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _simone.transform.LookAt(new Vector3(this.transform.position.x, _simone.transform.position.y, this.transform.position.z), _simone.transform.up);
                _theSimoneController.Movement = false;

                if (SceneToLoad != "")
                {
                    _sceneTrans.LoadScene(SceneToLoad);
                }
                else
                {
                    _fadingOut = true;

                    Invoke("MoveCharacter", FadeTime);
                }
            }
        }

        if(_fadingIn || _fadingOut)
        {
            ScreenFade();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Simone")
        {
            _nearDoor = false;

            if (DoorLocked)
                _theDialogUIController.ToggleLockedObj(false);
            else
                _theDialogUIController.ToggleInteractObj(false);
        }
    }

    private void ScreenFade()
    {
        _currentFadeMask = _theCamMan.CurrentCamera.GetComponentInChildren<SpriteRenderer>();

        if(_fadingOut)
        {
            _currentFadeMask.color = new Color(_currentFadeMask.color.r, _currentFadeMask.color.g, _currentFadeMask.color.b, _currentFadeMask.color.a + (Time.deltaTime * FadeTime));
            if(_currentFadeMask.color.a > 0.99f)
            {
                _fadingOut = false;
                _fadingIn = true;
            }
        }
        if(_fadingIn)
        {
            _currentFadeMask.color = new Color(_currentFadeMask.color.r, _currentFadeMask.color.g, _currentFadeMask.color.b, _currentFadeMask.color.a - (Time.deltaTime * FadeTime));

            if (_nextFadeMask)
            {
                _nextFadeMask.color = new Color(_nextFadeMask.color.r, _nextFadeMask.color.g, _nextFadeMask.color.b, _nextFadeMask.color.a - (Time.deltaTime * FadeTime));

                if (_currentFadeMask.color.a < 0.01f && _nextFadeMask.color.a < 0.01f)
                {
                    _fadingIn = false;
                    _theSimoneController.Movement = true;
                }
            }
            else
            {
                if (_currentFadeMask.color.a < 0.01f)
                {
                    _fadingIn = false;
                    _theSimoneController.Movement = true;
                }
            }
        }
    }

    private void MoveCharacter()
    {
        DoorController test = this;
        _simone.transform.position = MoveToTransform.position;
        //Mason: FIX THIS!!
        //_simone.transform.rotation = new E(_simone.transform.rotation.x, MoveToTransform.rotation.y, _simone.transform.rotation.z);
        _theCamMan.ActivateCam(MoveToCamera);
        _nextFadeMask = MoveToCamera.GetComponentInChildren<SpriteRenderer>();
        _nextFadeMask.color = new Color(_nextFadeMask.color.r, _nextFadeMask.color.g, _nextFadeMask.color.b, 1);
        _nearDoor = false;
    }
}