using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SuccessfulEncounter : EventGoal
{
    public string EncounterSubject,
                  EncounterGoal;

    [MenuItem("Assets/Create/EventGoals/SuccessfulEncounter")]
    public static void CreateEvent()
    {
        SuccessfulEncounter asset = ScriptableObject.CreateInstance<SuccessfulEncounter>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventGoals/NewSuccessfulEncounter.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}