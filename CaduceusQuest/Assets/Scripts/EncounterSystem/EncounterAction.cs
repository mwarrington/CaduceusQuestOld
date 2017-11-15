using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EncounterAction : ScriptableObject
{
    public EncounterActionType MyType;
    public float FailPenalty;
    public string Name;

    //public void GenericValuesForGUI()
    //{
    //    Name = EditorGUILayout.TextField("Name", Name);
    //    MyType = (EncounterActionType)EditorGUILayout.EnumPopup("Action Type", MyType);
    //    FailPenalty = EditorGUILayout.FloatField("Fail Penalty", FailPenalty);
    //    EditorGUILayout.Space();
    //}

    //private ScriptableObject _myScriptableObject;

    //public EncounterAction(EncounterActionType myType, float failPenalty, string name)
    //{
    //    MyType = myType;
    //    FailPenalty = failPenalty;
    //    Name = name;

    //    _myScriptableObject = Resources.Load<ScriptableObject>("EncounterActions/" + myType.ToString() + "EA" + name);
    //}

    //public void InitiateAction()
    //{
    //    
    //}
}