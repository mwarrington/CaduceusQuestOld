using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimoneController : MovementController
{
    protected bool moving
    {
        get
        {
            if (movingOnXAxis || movingOnZAxis)
                _moving = true;
            else
                _moving = false;

            return _moving;
        }
    }
    protected bool movingOnXAxis
    {
        get
        {
            if (_movingLeft || _movingRight)
                _movingOnXAxis = true;
            else
                _movingOnXAxis = false;

            return _movingOnXAxis;
        }
    }
    protected bool movingOnZAxis
    {
        get
        {
            if (_movingDown || _movingUp)
                _movingOnZAxis = true;
            else
                _movingOnZAxis = false;

            return _movingOnZAxis;
        }
    }
    private bool _moving;

    private Animator _myAnimator;
    private CameraManager _theCamMan;
    private float _idleTimer;
    private bool _movingOnXAxis,
                 _movingOnZAxis,
                 _movingRight,
                 _movingLeft,
                 _movingUp,
                 _movingDown;
	public bool Movement = true;

    private void Start()
    {
        _theCamMan = FindObjectOfType<CameraManager>();
        _myAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Movement)
        {
            InputHandler();

            _idleTimer += Time.deltaTime;
            if (_idleTimer > 5 && !_myAnimator.GetBool("longIdle"))
            {
                _myAnimator.SetBool("longIdle", true);
            }
        }

        if (moving)
            _myAnimator.SetFloat("speed", CurrentSpeed);
        else
            _myAnimator.SetFloat("speed", 0);
    }

    private void InputHandler()
    {
        //Movement Control
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && !movingOnZAxis)
        {
            if (movingOnXAxis)
            {
                CurrentSpeed /= 2;
            }
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !movingOnXAxis)
        {
            if (movingOnZAxis)
                CurrentSpeed /= 2;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(CardinalDirections.FORWARD);
            _movingUp = true;
        }
        else
        {
            _movingUp = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(CardinalDirections.BACKWARD);
            _movingDown = true;
        }
        else
        {
            _movingDown = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(CardinalDirections.LEFT);
            _movingLeft = true;
        }
        else
        {
            _movingLeft = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(CardinalDirections.RIGHT);
            _movingRight = true;
        }
        else
        {
            _movingRight = false;
        }

        running = Input.GetKey(KeyCode.LeftShift);

        if((Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)))
        {
            if (_movingOnXAxis)
                CurrentSpeed *= 2;

            _movingOnZAxis = false;
        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            if (_movingOnZAxis)
                CurrentSpeed *= 2;

            _movingOnXAxis = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _idleTimer = 0;
            _myAnimator.SetBool("longIdle", false);
        }
    }

    protected override void Move(CardinalDirections dir)
    {
        Vector3 trajectory = new Vector3();
        bool movingDiagonal = false;

        if (dir == CardinalDirections.FORWARD)
        {
            if (!movingOnXAxis)
                trajectory = new Vector3(_theCamMan.CurrentCamera.transform.forward.x, 0, _theCamMan.CurrentCamera.transform.forward.z);
            else if(_movingLeft)
            {
                trajectory = new Vector3(_theCamMan.CurrentCamera.transform.forward.x, 0, _theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(-_theCamMan.CurrentCamera.transform.right.x, 0, -_theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
            else if (_movingRight)
            {
                trajectory = new Vector3(_theCamMan.CurrentCamera.transform.forward.x, 0, _theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(_theCamMan.CurrentCamera.transform.right.x, 0, _theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
        }

        if (dir == CardinalDirections.BACKWARD)
        {
            if (!movingOnXAxis)
                trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.forward.x, 0, -_theCamMan.CurrentCamera.transform.forward.z);
            else if (_movingLeft)
            {
                trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.forward.x, 0, -_theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(-_theCamMan.CurrentCamera.transform.right.x, 0, -_theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
            else if (_movingRight)
            {
                trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.forward.x, 0, -_theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(_theCamMan.CurrentCamera.transform.right.x, 0, _theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
        }

        if (dir == CardinalDirections.RIGHT)
        {
            if (!movingOnZAxis)
                trajectory = new Vector3(_theCamMan.CurrentCamera.transform.right.x, 0, _theCamMan.CurrentCamera.transform.right.z);
            else if (_movingUp)
            {
                trajectory = new Vector3(_theCamMan.CurrentCamera.transform.forward.x, 0, _theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(_theCamMan.CurrentCamera.transform.right.x, 0, _theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
            else if (_movingDown)
            {
                trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.forward.x, 0, -_theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(_theCamMan.CurrentCamera.transform.right.x, 0, _theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
        }

        if (dir == CardinalDirections.LEFT)
        {
            if (!movingOnZAxis)
                trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.right.x, 0, -_theCamMan.CurrentCamera.transform.right.z);
            else if (_movingUp)
            {
                trajectory = new Vector3(_theCamMan.CurrentCamera.transform.forward.x, 0, _theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(-_theCamMan.CurrentCamera.transform.right.x, 0, -_theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
            else if (_movingDown)
            {
                trajectory = new Vector3(-_theCamMan.CurrentCamera.transform.forward.x, 0, -_theCamMan.CurrentCamera.transform.forward.z) +
                             new Vector3(-_theCamMan.CurrentCamera.transform.right.x, 0, -_theCamMan.CurrentCamera.transform.right.z);
                movingDiagonal = true;
            }
        }

        this.transform.LookAt(this.transform.position + (trajectory), this.transform.up);

        if (movingDiagonal)
            this.transform.Translate(this.transform.forward * (CurrentSpeed / 2) * Time.deltaTime, Space.World);
        else
            this.transform.Translate(this.transform.forward * CurrentSpeed * Time.deltaTime, Space.World);
    }
}