using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MovementController
{
    public Vector2 CameraFollowDistance;
    public bool FollowingSubject;
    private SimoneController _followSubject;
    private float _initialCameraDistance;

    void Start()
    {
        _followSubject = FindObjectOfType<SimoneController>();

        _initialCameraDistance = _followSubject.transform.position.z - this.transform.position.z;
    }

    void Update()
    {
        if (FollowingSubject)
            FollowSubject();
    }

    private void FollowSubject()
    {
        //Follow Right
        if((this.transform.position.x + CameraFollowDistance.x) < _followSubject.transform.position.x)
        {
            Move(CardinalDirections.RIGHT);
        }
        else if((this.transform.position.x - CameraFollowDistance.x) > _followSubject.transform.position.x) //Follow Left
        {
            Move(CardinalDirections.LEFT);
        }

        if (((this.transform.position.z + _initialCameraDistance) + CameraFollowDistance.y) < _followSubject.transform.position.z) //Follow Forward
        {
            Move(CardinalDirections.FORWARD);
        }
        else if (((this.transform.position.z + _initialCameraDistance) - CameraFollowDistance.y) > _followSubject.transform.position.z) //Follow Backward
        {
            Move(CardinalDirections.BACKWARD);
        }
    }

    protected override void Move(CardinalDirections dir)
    {
        float myCurrentSpeed = CurrentSpeed,
              orriginalSpeed = CurrentSpeed;

        if(dir == CardinalDirections.FORWARD)
        {
            myCurrentSpeed *= Mathf.Clamp(Mathf.Abs(((this.transform.position.z + _initialCameraDistance) + CameraFollowDistance.y) - _followSubject.transform.position.z), 0.05f, 1.1f);
        }

        if (dir == CardinalDirections.BACKWARD)
        {
            myCurrentSpeed *= Mathf.Clamp(Mathf.Abs(((this.transform.position.z + _initialCameraDistance) - CameraFollowDistance.y) - _followSubject.transform.position.z), 0.05f, 1.1f);
        }

        if (dir == CardinalDirections.RIGHT)
        {
            myCurrentSpeed *= Mathf.Clamp(Mathf.Abs((this.transform.position.x + CameraFollowDistance.x) - _followSubject.transform.position.x), 0.05f, 1.1f);
        }

        if (dir == CardinalDirections.LEFT)
        {
            myCurrentSpeed *= Mathf.Clamp(Mathf.Abs((this.transform.position.x - CameraFollowDistance.x) - _followSubject.transform.position.x), 0.05f, 1.1f);
        }

        if (myCurrentSpeed > _followSubject.CurrentSpeed)
        {
            myCurrentSpeed = _followSubject.CurrentSpeed;
        }

        CurrentSpeed = myCurrentSpeed;

        base.Move(dir);

        CurrentSpeed = orriginalSpeed;
    }
}