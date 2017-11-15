using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SuccessfulEncounter : EventGoal
{
    public string EncounterSubject,
                  EncounterGoal;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/EventGoals/SuccessfulEncounter")]
    public static void CreateEvent()
    {
        SuccessfulEncounter asset = ScriptableObject.CreateInstance<SuccessfulEncounter>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventGoals/NewSuccessfulEncounter.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
#endif
}