using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class EncounterGoal
{
    public string Subject,
                  ActionName;
    public float IndividualTrustMin,
                 InitialTrust;
    public int TreatmentCount;
    public EncounterActionType ActionType;

    public EncounterGoal()
    {
        Subject = "";
        ActionName = "";
        IndividualTrustMin = 0;
        TreatmentCount = 0;
        ActionType = EncounterActionType.DIALOG;
    }
}