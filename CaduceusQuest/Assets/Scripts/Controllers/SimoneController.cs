using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimoneController : MovementController
{
    private CameraManager _theCamMan;
    private float _currentSpeed;
    private bool _movingOnXAxis,
                 _movingOnZAxis;
	public bool Movement = true;

    private void Start()
    {
        _theCamMan = FindObjectOfType<CameraManager>();
        _currentSpeed = Speed;

    }

    void Update()
    {
		if (Movement) {
			InputHandler ();  
		}
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

    protected override void Move(CardinalDirections dir)
    {
        Vector3 trajectory = new Vector3();

        if (dir == CardinalDirections.FORWARD)
        {
            trajectory = new Vector3(_theCamMan.CurrentCamera.transform.forward.x, 0, _theCamMan.CurrentCamera.transform.forward.z);
        }

        if (dir == CardinalDirections.BACKWARD)
        {
            trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.forward.x, 0, -_theCamMan.CurrentCamera.transform.forward.z);
        }

        if (dir == CardinalDirections.LEFT)
        {
            trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.right.x, 0, -_theCamMan.CurrentCamera.transform.right.z);
        }

        if (dir == CardinalDirections.RIGHT)
        {
            trajectory = new Vector3(_theCamMan.CurrentCamera.transform.right.x, 0, _theCamMan.CurrentCamera.transform.right.z);
        }

        this.transform.Translate(trajectory * Speed * Time.deltaTime);
    }
}