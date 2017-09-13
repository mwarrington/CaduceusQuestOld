using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeDialog : EventAction
{
    public List<string> NPCsToUpdate;

    public override void OnActivate()
    {
        base.OnActivate();

        GameManager.TheGameManager.DialogueChanger(DialogChangeType.EVENTTRIGGER, NPCsToUpdate);
    }

    [MenuItem("Assets/Create/EventActions/ChangeDialog")]
    public static void CreateEvent()
    {
        ChangeDialog asset = ScriptableObject.CreateInstance<ChangeDialog>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventActions/NewChangeDialogAction.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}