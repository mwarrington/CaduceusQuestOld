using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EncounterGoal
{
    public string Subject,
                  ActionName;
    public float IndividualTrustMin;
    public EncounterActionType ActionType;

    public EncounterGoal()
    {
        Subject = "";
        ActionName = "";
        IndividualTrustMin = 0;
        ActionType = EncounterActionType.DIALOG;
    }
}

public class Encounter : ScriptableObject
{
    public int GoalCount;
    public EncounterPattern TurnPattern;
    public EncounterGoal[] EncounterGoals;

    void Awake()
    {
        EncounterGoals = new EncounterGoal[GoalCount];

        for (int i = 0; i < 5; i++)
        {
            EncounterGoals[i] = new EncounterGoal();
        }
    }
}

public class MakeEncounterObj
{
    class MakeEncounterWindow : EditorWindow
    {
        int goalCount = 1;
        EncounterPattern turnPattern;
        EncounterGoal[] encounterGoals = new EncounterGoal[5];

        void Awake()
        {
            for (int i = 0; i < 5; i++)
            {
                encounterGoals[i] = new EncounterGoal();
            }
        }

        [MenuItem("Assets/Create/Encouner")]
        public static void OpenMakeEncounterWindow()
        {
            EditorWindow.GetWindow(typeof(MakeEncounterWindow));
        }

        private void ShowEncounterGoalOptions(int i)
        {
            EditorGUILayout.LabelField("Encounter Goal " + i);
            encounterGoals[i].Subject = EditorGUILayout.TextField("Subject", encounterGoals[i].Subject);
            encounterGoals[i].ActionName = EditorGUILayout.TextField("ActionName", encounterGoals[i].ActionName);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Trust Threshold");
            encounterGoals[i].IndividualTrustMin = EditorGUILayout.FloatField("Individual Trust Minimum", encounterGoals[i].IndividualTrustMin);
            EditorGUILayout.Space();

            encounterGoals[i].ActionType = (EncounterActionType)EditorGUILayout.EnumPopup("Encounter Action Type", encounterGoals[i].ActionType);
        }

        void OnGUI()
        {
            goalCount = EditorGUILayout.IntField("Goal Count", goalCount);
            EditorGUILayout.Space();

            turnPattern = (EncounterPattern)EditorGUILayout.EnumPopup("Encounter Turn Pattern", turnPattern);
            EditorGUILayout.Space();

            //GIVE 'EM THE CLAMPS!!
            goalCount = Mathf.Clamp(goalCount, 1, 5);

            if (goalCount == 1)
            {
                #region 1Goals
                ShowEncounterGoalOptions(0);
                #endregion 1Goal
            }
            else if (goalCount == 2)
            {
                #region 2Goals
                ShowEncounterGoalOptions(0);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(1);
                #endregion 2Goal
            }
            else if (goalCount == 3)
            {
                #region 3Goals
                ShowEncounterGoalOptions(0);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(1);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(2);
                #endregion 3Goal
            }
            else if (goalCount == 4)
            {
                #region 4Goals
                ShowEncounterGoalOptions(0);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(1);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(2);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(3);
                #endregion 4Goal
            }
            else if (goalCount == 5)
            {
                #region 5Goals
                ShowEncounterGoalOptions(0);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(1);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(2);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(3);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ShowEncounterGoalOptions(4);
                #endregion 5Goal
            }

            if (GUILayout.Button("Create"))
            {
                CreateEncounterObj(goalCount, turnPattern, encounterGoals);
            }
        }

        public static void CreateEncounterObj(int goalCount, EncounterPattern turnPattern, EncounterGoal[] encounterGoals)
        {
            Encounter theEncounter = ScriptableObject.CreateInstance<Encounter>();

            //for (int i = 0; i < goalCount; i++)
            //{
            //    theEncounter.EncounterGoals[i] = encounterGoals[i];
            //}

            theEncounter.EncounterGoals = encounterGoals;
            theEncounter.TurnPattern = turnPattern;
            theEncounter.GoalCount = goalCount;

            AssetDatabase.CreateAsset(theEncounter, "Assets/Resources/EncounterData/NewEcounterData.asset");
            AssetDatabase.SaveAssets();

            Selection.activeObject = theEncounter;
        }
    }
}