using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CompleteConversation : EventGoal
{
    public string Name;
    public char Index;

    [MenuItem("Assets/Create/EventGoals/CompleteConvorsation")]
    public static void CreateEvent()
    {
        CompleteConversation asset = ScriptableObject.CreateInstance<CompleteConversation>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventGoals/NewCompleteConvorsation.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}