using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Speed;    

    protected virtual void Move(CardinalDirections dir)
    {
        if (dir == CardinalDirections.FORWARD)
        {
            this.transform.Translate(0, 0, Speed * Time.deltaTime);
        }

        if (dir == CardinalDirections.BACKWARD)
        {
            this.transform.Translate(0, 0, -Speed * Time.deltaTime);
        }

        if (dir == CardinalDirections.LEFT)
        {
            this.transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }

        if (dir == CardinalDirections.RIGHT)
        {
            this.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
    }
}