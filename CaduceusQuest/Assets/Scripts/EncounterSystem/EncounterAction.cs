using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EncounterAction
{
    public EncounterActionType MyType;
    public string Name;
    public string EncounterMessage;

    public void BeginDialogEvent(string encounterSubj)
    {
        
    }
}

public class EncounterActionDialog : ScriptableObject
{
    public string Speaker,
                  SpeakerLine,
                  BadResponse,
                  MedResponse,
                  GoodResponse;
    public Emotion LineEmotion = new Emotion('a',0);
}

public class MakeEncounterActionDialog
{
    class EADialogWindow : EditorWindow
    {
        string speaker,
               speakerLine,
               badResponse,
               medResponse,
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
            speaker = EditorGUILayout.TextField("Speaker", speaker);
            EditorGUILayout.Space();

            speakerLine = EditorGUILayout.TextField("Speaker Line", speakerLine);
            badResponse = EditorGUILayout.TextField("Bad Response", badResponse);
            medResponse = EditorGUILayout.TextField("Medium Response", medResponse);
            goodResponse = EditorGUILayout.TextField("Good Response", goodResponse);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Emotion");
            lineEmotionType = EditorGUILayout.TextField("EmotionType", lineEmotionType);
            lineEmotionIntensity = EditorGUILayout.IntField("Emotion Intensity", lineEmotionIntensity);
            EditorGUILayout.Space();

            if (GUILayout.Button("Create"))
            {
                CreateEADialog(speaker, speakerLine, badResponse, medResponse, goodResponse, char.Parse(lineEmotionType), lineEmotionIntensity);
            }
        }
    }

    public static void CreateEADialog(string speaker, string speakerLine, string badResponse, string medResponse, string goodResponse, char emotionType, int emotionIntensity)
    {
        EncounterActionDialog asset = ScriptableObject.CreateInstance<EncounterActionDialog>();

        asset.Speaker = speaker;
        asset.SpeakerLine = speakerLine;
        asset.BadResponse = badResponse;
        asset.MedResponse = medResponse;
        asset.GoodResponse = goodResponse;
        asset.LineEmotion.EmotionType = emotionType;
        asset.LineEmotion.EmotionIntensity = emotionIntensity;

        AssetDatabase.CreateAsset(asset, "Assets/EncounterActions/NewEncounterActionDialog.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}