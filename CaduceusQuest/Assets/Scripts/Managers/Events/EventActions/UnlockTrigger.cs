using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnlockTrigger : EventAction
{
    public List<string> TriggersToUnlock = new List<string>(),
                        TriggersToLock = new List<string>();

    public override void OnActivate()
    {
        base.OnActivate();

        GameManager.TheGameManager.SetWorldTriggersToChange(TriggersToUnlock, TriggersToLock);

        //if (TriggersToUnlock.Count > 0)
        //{
        //    for (int i = 0; i < TriggersToUnlock.Count; i++)
        //    {
        //        GameObject.Find(TriggersToUnlock[i]).GetComponent<Collider>().enabled = true;
        //    }
        //}

        //if (TriggersToLock.Count > 0)
        //{
        //    for (int i = 0; i < TriggersToLock.Count; i++)
        //    {
        //        GameObject.Find(TriggersToLock[i]).GetComponent<Collider>().enabled = false;
        //    }
        //}
    }

    [MenuItem("Assets/Create/EventActions/UnlockTrigger")]
    public static void CreateEvent()
    {
        UnlockTrigger asset = ScriptableObject.CreateInstance<UnlockTrigger>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/EventActions/NewUnlockTriggerAction.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}