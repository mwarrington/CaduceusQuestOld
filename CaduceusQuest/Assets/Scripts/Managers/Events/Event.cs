using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Event : ScriptableObject
{
    public string Name;
    public List<EventGoal> EventGoals = new List<EventGoal>();
    public int GoalCount;

    public void GoalReached(string NPC, string Treatment)
    {
        for (int i = 0; i < GoalCount; i++)
        {
            if (EventGoals[i].NPC == NPC && EventGoals[i].Treatment == Treatment)
            {
                EventGoals[i].Achieved = true;

                break;
            }
        }
    }
}

public class MakeEvent
{
    class EventWindow : EditorWindow
    {
        string myName;
        List<EventGoal> eventGoals = new List<EventGoal>();
        int goalCount = 3;

        private void Awake()
        {
            EventGoal[] goals = new EventGoal[goalCount];
            eventGoals.AddRange(goals);
        }

        [MenuItem("Assets/Create/Event")]
        public static void OpenEventWindow()
        {
            EditorWindow.GetWindow(typeof(EventWindow));
        }

        private void EventGoalBit(int i)
        {
            if(goalCount > eventGoals.Count)
            {
                for (int j = eventGoals.Count; j < goalCount; j++)
                {
                    eventGoals.Add(new EventGoal());
                }
            }
            else if(goalCount < eventGoals.Count)
            {
                for (int j = eventGoals.Count; j > goalCount; j--)
                {
                    eventGoals.Remove(eventGoals[eventGoals.Count - 1]);
                }
            }

            EditorGUILayout.LabelField("Goal " + (i + 1));
            eventGoals[i].NPC = EditorGUILayout.TextField("Event NPC", eventGoals[i].NPC);
            eventGoals[i].Treatment = EditorGUILayout.TextField("Event Treatment", eventGoals[i].Treatment);
            EditorGUILayout.Space();
        }

        private void OnGUI()
        {
            myName = EditorGUILayout.TextField("Event Name", myName);
            goalCount = EditorGUILayout.IntField("Goal Count", goalCount);

            for (int i = 0; i < goalCount; i++)
            {
                EventGoalBit(i);
            }

            if (GUILayout.Button("Create"))
            {
                CreateEvent(myName, goalCount, eventGoals);
            }
        }
    }

    public static void CreateEvent(string myName, int goalCount, List<EventGoal> eventGoals)
    {
        Event asset = ScriptableObject.CreateInstance<Event>();

        asset.Name = myName;
        asset.GoalCount = goalCount;
        asset.EventGoals = eventGoals;

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Events/" + myName + ".asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}

[System.Serializable]
public class EventGoal
{
    public string NPC,
                  Treatment;
    public bool Achieved;

    public EventGoal()
    {
        NPC = "";
        Treatment = "";
        Achieved = false;
    }
}