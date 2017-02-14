using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimoneController : MovementController
{
    void Update()
    {
        InputHandler();   
    }

    private void InputHandler()
    {
        //Movement Control
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
    }
}