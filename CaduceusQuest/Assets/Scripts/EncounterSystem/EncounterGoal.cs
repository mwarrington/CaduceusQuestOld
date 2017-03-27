using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterGoal
{
    public string Subject,
                  ActionName;
    public float IndividualTrust;
    public EncounterActionType ActionType;
}

public class Encounter : ScriptableObject
{
    public List<EncounterGoal> EncounterGoals = new List<EncounterGoal>();
}

public class MakeEncounterObj
{
    //Make a context menu that creates new Encounter scriptable objects
}