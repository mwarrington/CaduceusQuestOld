using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Event : ScriptableObject
{
    public string Name;
    public List<string> Subjects;
    public List<EventGoal> EventGoals = new List<EventGoal>();

    public void GoalReached(string NPC, string Treatment)
    {
        for (int i = 0; i < EventGoals.Count; i++)
        {
            if (EventGoals[i].NPC == NPC && EventGoals[i].Treatment == Treatment)
            {
                EventGoals[i].Achieved = true;
                AllGoalsReached();

                break;
            }
        }
    }

    private void AllGoalsReached()
    {
        bool goalsReached = true;
        for (int j = 0; j < EventGoals.Count; j++)
        {
            if(!EventGoals[j].Achieved)
            {
                goalsReached = false;
                break;
            }
        }
        if(goalsReached)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            for (int j = 0; j < Subjects.Count; j++)
            {
                gameManager.DialogueChanger(Subjects[j], DialogChangeType.EVENTTRIGGER);
            }
        }
    }
}

public class MakeEvent
{
    class EventWindow : EditorWindow
    {
        string myName;
        List<string> subjects = new List<string>();
        List<EventGoal> eventGoals = new List<EventGoal>();
        int goalCount = 3,
            subjectCount = 2;

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

        private void SubjectsBit(int i)
        {
            if(subjectCount > subjects.Count)
            {
                for (int j = subjects.Count; j < subjectCount ; j++)
                {
                    subjects.Add("");
                }
            }
            else if(subjectCount < subjects.Count)
            {
                for (int j = subjects.Count; j < subjectCount; j++)
                {
                    subjects.Remove(subjects[subjects.Count - 1]);
                }
            }

            subjects[i] = EditorGUILayout.TextField("Subject " + (i + 1) + " Name", subjects[i]);
            EditorGUILayout.Space();
        }

        private void OnGUI()
        {
            myName = EditorGUILayout.TextField("Event Name", myName);
            EditorGUILayout.Space();

            subjectCount = EditorGUILayout.IntField("Subject Count", subjectCount);
            for (int i = 0; i < subjectCount; i++)
            {
                SubjectsBit(i);
            }

            goalCount = EditorGUILayout.IntField("Goal Count", goalCount);

            for (int i = 0; i < goalCount; i++)
            {
                EventGoalBit(i);
            }

            if (GUILayout.Button("Create"))
            {
                CreateEvent(myName, subjects, eventGoals);
            }
        }
    }

    public static void CreateEvent(string myName, List<string> subjects, List<EventGoal> eventGoals)
    {
        Event asset = ScriptableObject.CreateInstance<Event>();

        asset.Name = myName;
        asset.Subjects = subjects;
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