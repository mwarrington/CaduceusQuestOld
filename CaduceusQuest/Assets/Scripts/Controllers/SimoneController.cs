using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimoneController : MovementController
{
    private float _currentSpeed;
    private bool _movingOnXAxis,
                 _movingOnZAxis;

    private void Start()
    {
        _currentSpeed = Speed;
    }

    void Update()
    {
        InputHandler();   
    }

    private void InputHandler()
    {
        //Movement Control
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && !_movingOnZAxis)
        {
            if(_movingOnXAxis)
                _currentSpeed /= 2;

            _movingOnZAxis = true;
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !_movingOnXAxis)
        {
            if (_movingOnZAxis)
                _currentSpeed /= 2;

            _movingOnXAxis = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(CardinalDirections.FORWARD);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(CardinalDirections.BACKWARD);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(CardinalDirections.LEFT);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(CardinalDirections.RIGHT);
        }

        if((Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)))
        {
            if (_movingOnXAxis)
                _currentSpeed *= 2;

            _movingOnZAxis = false;
        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            if (_movingOnZAxis)
                _currentSpeed *= 2;

            _movingOnXAxis = false;
        }
    }
}