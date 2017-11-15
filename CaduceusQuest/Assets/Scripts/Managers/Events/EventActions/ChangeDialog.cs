using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeDialog : EventAction
{
    public List<string> NPCsToUpdate;
    public List<char> NewDialogIndexes;

    public override void OnActivate()
    {
        base.OnActivate();

        GameManager.TheGameManager.EventDialogueChanger(NPCsToUpdate, NewDialogIndexes);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/EventActions/ChangeDialog")]
    public static void CreateEvent()
    {
        ChangeDialog asset = ScriptableObject.CreateInstance<ChangeDialog>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventActions/NewChangeDialogAction.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
#endif
}