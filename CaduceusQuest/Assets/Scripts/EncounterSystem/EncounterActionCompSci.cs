using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EncounterActionCompSci : EncounterAction
{
    public int SymbolCount,
               StrikeCount;
}

public class MakeEncounterActionCompSci
{
    class EACompSciWindow : EditorWindow
    {
        EncounterActionType myType = EncounterActionType.COMPSCI;
        float failPenalty;
        string name;
        int symbolCount,
            strikeCount;

        [MenuItem("Assets/Create/EncounerAction/CompSci")]
        public static void OpenEACompSciWindow()
        {
            EditorWindow.GetWindow(typeof(EACompSciWindow));
        }

        void OnGUI()
        {
            name = EditorGUILayout.TextField("Name", name);
            failPenalty = EditorGUILayout.FloatField("Fail Penalty", failPenalty);
            EditorGUILayout.Space();

            symbolCount = EditorGUILayout.IntField("Symbol Count", symbolCount);
            strikeCount = EditorGUILayout.IntField("Strike Count", strikeCount);
            EditorGUILayout.Space();

            if (GUILayout.Button("Create"))
            {
                CreateEACompSci(symbolCount, strikeCount, name, failPenalty);
            }
        }

        public static void CreateEACompSci(int symbolCount, int strikeCount, string name, float failPenalty)
        {
            EncounterActionCompSci asset = ScriptableObject.CreateInstance<EncounterActionCompSci>();

            asset.Name = name;
            asset.MyType = EncounterActionType.COMPSCI;
            asset.FailPenalty = failPenalty;
            asset.SymbolCount = symbolCount;
            asset.StrikeCount = strikeCount;

            AssetDatabase.CreateAsset(asset, "Assets/Resources/EncounterActions/CompSciEA.asset");
            AssetDatabase.SaveAssets();

            Selection.activeObject = asset;
        }
    }
}
