using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EncounterActionDoctor : EncounterAction
{
    public int KeyStrokeCount;
    public float ArrowMinSpeed,
                 ArrowMaxSpeed,
                 SpawnRate;
}

#if UNITY_EDITOR
public class MakeEncounterActionDoctor
{
    class EADoctorWindow : EditorWindow
    {
        EncounterActionType myType = EncounterActionType.DOCTOR;
        float failPenalty;
        string name;
        int keyStrokeCount;
        float arrowMinSpeed,
              arrowMaxSpeed,
              spawnRate;

        [MenuItem("Assets/Create/EncounerAction/Doctor")]
        public static void OpenEACompSciWindow()
        {
            EditorWindow.GetWindow(typeof(EADoctorWindow));
        }

        void OnGUI()
        {
            name = EditorGUILayout.TextField("Name", name);
            failPenalty = EditorGUILayout.FloatField("Fail Penalty", failPenalty);
            EditorGUILayout.Space();

            keyStrokeCount = EditorGUILayout.IntField("Key Stroke Count", keyStrokeCount);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Arrow Speed");
            arrowMinSpeed = EditorGUILayout.FloatField("Minimum Arrow Speed", arrowMinSpeed);
            arrowMaxSpeed = EditorGUILayout.FloatField("Maximum Arrow Speed", arrowMaxSpeed);
            EditorGUILayout.Space();

            spawnRate = EditorGUILayout.FloatField("Spawn Rate", spawnRate);
            EditorGUILayout.Space();

            if (GUILayout.Button("Create"))
            {
                CreateEADoctor(keyStrokeCount, arrowMinSpeed, arrowMaxSpeed, spawnRate, name, failPenalty);
            }
        }

        public static void CreateEADoctor(int keyStrokeCount, float arrowMinSpeed, float arrowMaxSpeed, float spawnRate, string name, float failPenalty)
        {
            EncounterActionDoctor asset = ScriptableObject.CreateInstance<EncounterActionDoctor>();

            asset.Name = name;
            asset.MyType = EncounterActionType.DOCTOR;
            asset.FailPenalty = failPenalty;
            asset.KeyStrokeCount = keyStrokeCount;
            asset.ArrowMinSpeed = arrowMinSpeed;
            asset.ArrowMaxSpeed = arrowMaxSpeed;
            asset.SpawnRate = spawnRate;

            AssetDatabase.CreateAsset(asset, "Assets/Resources/EncounterActions/DoctorEA.asset");
            AssetDatabase.SaveAssets();

            Selection.activeObject = asset;
        }
    }
}
#endif