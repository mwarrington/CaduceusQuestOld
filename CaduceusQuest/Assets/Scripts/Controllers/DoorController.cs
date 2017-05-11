using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Camera MoveToCamera;
    public Transform MoveToTransform;
    public float FadeTime;

    private CameraManager _theCamMan;
    private SpriteRenderer _currentFadeMask;
    private GameObject _simone;
    private bool _nearDoor,
                 _fadingIn,
                 _fadingOut;

    private void Start()
    {
        _theCamMan = FindObjectOfType<CameraManager>();
        Debug.Log(_theCamMan);
        _currentFadeMask = _theCamMan.CurrentCamera.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Simone")
        {
            _nearDoor = true;
            _simone = other.gameObject;
        }
    }

    private void Update()
    {
        if(_nearDoor)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                _fadingOut = true;

                Invoke("MoveCharacter", FadeTime);
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
            _nearDoor = true;
        }
    }

    private void ScreenFade()
    {
        _currentFadeMask = _theCamMan.CurrentCamera.GetComponentInChildren<SpriteRenderer>();

        if(_fadingOut)
        {
            _currentFadeMask.color = new Color(_currentFadeMask.color.r, _currentFadeMask.color.g, _currentFadeMask.color.b, _currentFadeMask.color.a + (Time.deltaTime * FadeTime));
            if(_currentFadeMask.color.a > 0.95f)
            {
                _fadingOut = false;
                _fadingIn = true;
            }
        }
        if(_fadingIn)
        {
            _currentFadeMask.color = new Color(_currentFadeMask.color.r, _currentFadeMask.color.g, _currentFadeMask.color.b, _currentFadeMask.color.a - (Time.deltaTime * FadeTime));
            if (_currentFadeMask.color.a < 0.05f)
            {
                _fadingIn = false;
            }
        }
    }

    private void MoveCharacter()
    {
        _simone.transform.position = MoveToTransform.position;
        _simone.transform.rotation = MoveToTransform.rotation;
        _theCamMan.ActivateCam(MoveToCamera);
    }
}