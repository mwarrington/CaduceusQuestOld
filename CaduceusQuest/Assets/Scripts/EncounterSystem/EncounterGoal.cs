using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EncounterGoal
{
    public string Subject,
                  ActionName;
    public float IndividualTrustGoal,
                 IndividualTrustMin;
    public EncounterActionType ActionType;
}

public class Encounter : ScriptableObject
{
    public int GoalCount;
    public List<EncounterGoal> EncounterGoals = new List<EncounterGoal>();
}

public class MakeEncounterObj
{
    class MakeEncounterWindow : EditorWindow
    {
        int goalCount;
        string subject,
               actionName;
        float individualTrustGoal,
              individualTrustMin;
        EncounterActionType actionType;

        [MenuItem("Assets/Create/Encouner")]
        public static void OpenMakeEncounterWindow()
        {
            EditorWindow.GetWindow(typeof(MakeEncounterWindow));
        }

        void OnGUI()
        {
            goalCount = EditorGUILayout.IntField("Goal Count", goalCount);
            EditorGUILayout.Space();

            if (goalCount == 1)
            {
                #region 1Goals
                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                #endregion 1Goal
            }
            else if (goalCount == 2)
            {
                #region 2Goals
                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                #endregion 2Goal
            }
            else if (goalCount == 3)
            {
                #region 3Goals
                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                #endregion 3Goal
            }
            else if (goalCount == 4)
            {
                #region 4Goals
                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                #endregion 4Goal
            }
            else if (goalCount == 5)
            {
                #region 5Goals
                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                subject = EditorGUILayout.TextField("Subject", subject);
                actionName = EditorGUILayout.TextField("ActionName", actionName);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Trust Threshold");
                individualTrustGoal = EditorGUILayout.FloatField("Individual Trust Goal", individualTrustGoal);
                individualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", individualTrustMin);
                EditorGUILayout.Space();

                actionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", actionType);
                #endregion 5Goal
            }
        }

        public static void CreateEncounterObj()
        {
            Encoun
        }
    }
}