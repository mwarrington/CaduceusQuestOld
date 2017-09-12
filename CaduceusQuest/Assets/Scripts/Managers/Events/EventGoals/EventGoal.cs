using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGoal : ScriptableObject
{
    public bool Achieved
    {
        get
        {
            return _achieved;
        }

        set
        {
            _achieved = value;
        }
    }
    private bool _achieved;
}