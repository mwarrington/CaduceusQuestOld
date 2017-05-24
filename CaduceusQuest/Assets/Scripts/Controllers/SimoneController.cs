using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimoneController : MovementController
{
    protected bool moving
    {
        get
        {
            if (_movingOnXAxis || _movingOnZAxis)
                _moving = true;
            else
                _moving = false;

            return _moving;
        }
    }
    private bool _moving;

    private Animator _myAnimator;
    private CameraManager _theCamMan;
    private float _currentSpeed;
    private bool _movingOnXAxis,
                 _movingOnZAxis;
	public bool Movement = true;

    private void Start()
    {
        _theCamMan = FindObjectOfType<CameraManager>();
        _myAnimator = this.GetComponent<Animator>();
        _currentSpeed = CurrentSpeed;
    }

    void Update()
    {
        if (Movement)
        {
            InputHandler();
        }

        if (moving)
            _myAnimator.SetFloat("speed", CurrentSpeed);
        else
            _myAnimator.SetFloat("speed", 0);
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

        running = Input.GetKey(KeyCode.LeftShift);

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

        this.transform.Translate(trajectory * CurrentSpeed * Time.deltaTime);
    }
}