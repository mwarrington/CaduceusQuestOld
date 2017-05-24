using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float WalkSpeed,
                 RunSpeed;

    public float CurrentSpeed
    {
        get
        {
            if(running)
                _currentSpeed = RunSpeed;
            else
                _currentSpeed = WalkSpeed;

            return _currentSpeed;
        }

        set
        {
            if(value != _currentSpeed)
            {
                _currentSpeed = value;
            }
        }
    }
    private float _currentSpeed;

    protected bool running;

    protected virtual void Move(CardinalDirections dir)
    {
        if (dir == CardinalDirections.FORWARD)
        {
            this.transform.Translate(0, 0, CurrentSpeed * Time.deltaTime);
        }

        if (dir == CardinalDirections.BACKWARD)
        {
            this.transform.Translate(0, 0, -CurrentSpeed * Time.deltaTime);
        }

        if (dir == CardinalDirections.LEFT)
        {
            this.transform.Translate(-CurrentSpeed * Time.deltaTime, 0, 0);
        }

        if (dir == CardinalDirections.RIGHT)
        {
            this.transform.Translate(CurrentSpeed * Time.deltaTime, 0, 0);
        }
    }
}