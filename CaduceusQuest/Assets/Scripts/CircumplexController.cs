using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircumplexController : MonoBehaviour
{
    private bool _draggingCircumplex;
    private float _initialMouseDragPosition;
    private Vector3 _initialRotation;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            _draggingCircumplex = true;
            _initialMouseDragPosition = Input.mousePosition.x;
            _initialRotation = this.transform.localEulerAngles;
        }

        if(_draggingCircumplex)
        {
            this.transform.localEulerAngles = new Vector3(_initialRotation.x, _initialRotation.y + (_initialMouseDragPosition - Input.mousePosition.x) / 5, _initialRotation.z);
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            _draggingCircumplex = false;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * 60);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.down * Time.deltaTime * 60);
        }
    }
}