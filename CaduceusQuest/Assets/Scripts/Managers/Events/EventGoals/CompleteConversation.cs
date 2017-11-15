using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CompleteConversation : EventGoal
{
    public string Name;
    public char Index;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/EventGoals/CompleteConvorsation")]
    public static void CreateEvent()
    {
        CompleteConversation asset = ScriptableObject.CreateInstance<CompleteConversation>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventGoals/NewCompleteConvorsation.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
#endif
}