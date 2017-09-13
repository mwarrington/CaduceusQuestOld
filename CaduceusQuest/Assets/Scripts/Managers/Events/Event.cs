using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Event : ScriptableObject
{
    public bool Complete
    {
        get
        {
            return _complete;
        }

        set
        {
            if (value)
            {
                foreach (EventAction ea in EventActions)
                {
                    ea.OnActivate();
                }
            }

            _complete = value;
        }
    }
    private bool _complete = false;

    public List<EventGoal> EventGoals = new List<EventGoal>();
    public List<EventAction> EventActions = new List<EventAction>();

    [MenuItem("Assets/Create/Event")]
    public static void CreateEvent()
    {
        Event asset = ScriptableObject.CreateInstance<Event>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/NewEvent.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }

    public void AssessEventGoals()
    {
        bool allAchieved = true;
        foreach (EventGoal eg in EventGoals)
        {
            if (!eg.Achieved)
                allAchieved = false;
        }

        if (allAchieved)
        {
            Complete = true;
            GameManager.TheGameManager.ProceedToNextEvent();
        }
    }
}