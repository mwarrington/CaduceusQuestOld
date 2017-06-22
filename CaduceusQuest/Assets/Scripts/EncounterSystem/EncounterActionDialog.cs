using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EncounterActionDialog : EncounterAction
{
    public string Speaker,
                  SpeakerLine,
                  BadResponse,
                  GoodResponse;
    public Emotion LineEmotion = new Emotion('a', 0);
}

public class MakeEncounterActionDialog
{
    class EADialogWindow : EditorWindow
    {
        EncounterActionType myType = EncounterActionType.DIALOG;
        float failPenalty;
        string name;
        string speaker,
               speakerLine,
               badResponse,
               goodResponse,
               lineEmotionType;

        int lineEmotionIntensity;

        [MenuItem("Assets/Create/EncounerAction/Dialog")]
        public static void OpenEADialogWindow()
        {
            EditorWindow.GetWindow(typeof(EADialogWindow));
        }

        void OnGUI()
        {
            name = EditorGUILayout.TextField("Name", name);
            failPenalty = EditorGUILayout.FloatField("Fail Penalty", failPenalty);
            EditorGUILayout.Space();

            speaker = EditorGUILayout.TextField("Speaker", speaker);
            EditorGUILayout.Space();

            speakerLine = EditorGUILayout.TextField("Speaker Line", speakerLine);
            badResponse = EditorGUILayout.TextField("Bad Response", badResponse);
            goodResponse = EditorGUILayout.TextField("Good Response", goodResponse);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Emotion");
            lineEmotionType = EditorGUILayout.TextField("EmotionType", lineEmotionType);
            lineEmotionIntensity = EditorGUILayout.IntField("Emotion Intensity", lineEmotionIntensity);
            EditorGUILayout.Space();

            if (GUILayout.Button("Create"))
            {
                CreateEADialog(speaker, speakerLine, badResponse, goodResponse, char.Parse(lineEmotionType), lineEmotionIntensity, name, failPenalty);
            }
        }
    }

    public static void CreateEADialog(string speaker, string speakerLine, string badResponse, string goodResponse, char emotionType, int emotionIntensity, string name, float failPenalty)
    {
        EncounterActionDialog asset = ScriptableObject.CreateInstance<EncounterActionDialog>();

        asset.Name = name;
        asset.MyType = EncounterActionType.DIALOG;
        asset.FailPenalty = failPenalty;
        asset.Speaker = speaker;
        asset.SpeakerLine = speakerLine;
        asset.BadResponse = badResponse;
        asset.GoodResponse = goodResponse;
        asset.LineEmotion.EmotionType = emotionType;
        asset.LineEmotion.EmotionIntensity = emotionIntensity;

        AssetDatabase.CreateAsset(asset, "Assets/Resources/EncounterActions/DialogEA.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}
