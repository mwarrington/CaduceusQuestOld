using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EncounterAction
{
    public EncounterActionType MyType;
    public float FailPenalty;
    public string Name;

    private ScriptableObject _myScriptableObject;

    public EncounterAction(EncounterActionType myType, float failPenalty, string name)
    {
        MyType = myType;
        FailPenalty = failPenalty;
        Name = name;

        _myScriptableObject = Resources.Load<ScriptableObject>("EncounterActions/" + myType.ToString() + "EA" + name);
    }

    public void InitiateAction()
    {
        switch(MyType)
        {
            case EncounterActionType.COMPSCI:
                EncounterActionCompSci myCompSciSO = (EncounterActionCompSci)_myScriptableObject;
                GameObject CompSciEAObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzle/CompSciPuzzle/CompSciPuzzle" + myCompSciSO.SymbolCount);
                CompSciEAObj = GameObject.Instantiate(CompSciEAObj);
                CompSciEAObj.GetComponent<CompSciPuzzleManager>().Strikes = myCompSciSO.StrikeCount;
                break;
            case EncounterActionType.DOCTOR:
                EncounterActionDoctor myDoctorSO = (EncounterActionDoctor)_myScriptableObject;
                GameObject DoctorPuzzleObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzle/Doctor/DoctorPuzzleDefault");
                DoctorPuzzleObj = GameObject.Instantiate(DoctorPuzzleObj);
                DoctorPuzzleManager myDocPuzzMan = DoctorPuzzleObj.GetComponent<DoctorPuzzleManager>();
                myDocPuzzMan.KeyStrokeCount = myDoctorSO.KeyStrokeCount;
                myDocPuzzMan.MinArrowSpeed = myDoctorSO.ArrowMinSpeed;
                myDocPuzzMan.MaxArrowSpeed = myDoctorSO.ArrowMaxSpeed;
                myDocPuzzMan.SpawnRate = myDoctorSO.SpawnRate;
                break;
            case EncounterActionType.DIALOG:
                EncounterActionDialog myDialogSO = (EncounterActionDialog)_myScriptableObject;
                GameObject DialogPuzzleObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzle/Dialog/DialogPuzzle");
                DialogPuzzleObj = GameObject.Instantiate(DialogPuzzleObj);
                DialogPuzzleManager myDialogPuzzMan = DialogPuzzleObj.GetComponent<DialogPuzzleManager>();
                myDialogPuzzMan.Speaker = myDialogSO.Speaker;
                myDialogPuzzMan.SpeakerLine = myDialogSO.SpeakerLine;
                myDialogPuzzMan.BadResponse = myDialogSO.BadResponse;
                myDialogPuzzMan.MedResponse = myDialogSO.MedResponse;
                myDialogPuzzMan.GoodResponse = myDialogSO.GoodResponse;
                myDialogPuzzMan.LineEmotion = myDialogSO.LineEmotion;
                break;
            default:
                Debug.LogError("We haven't put together an IntiateAction for that action type.");
                break;
        }
    }
}

#region Encounter Action Scriptable Objects
#region Dialog Encounter Action
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

        AssetDatabase.CreateAsset(asset, "Assets/Resources/EncounterActions/DialogEA.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}
#endregion Dialog Encounter Action

#region CompSci Encounter Action
public class EncounterActionCompSci : ScriptableObject
{
    public int SymbolCount,
               StrikeCount;
}

public class MakeEncounterActionCompSci
{
    class EACompSciWindow : EditorWindow
    {
        int symbolCount,
            strikeCount;

        [MenuItem("Assets/Create/EncounerAction/CompSci")]
        public static void OpenEACompSciWindow()
        {
            EditorWindow.GetWindow(typeof(EACompSciWindow));
        }

        void OnGUI()
        {
            symbolCount = EditorGUILayout.IntField("Symbol Count", symbolCount);
            strikeCount = EditorGUILayout.IntField("Strike Count", strikeCount);
            EditorGUILayout.Space();

            if (GUILayout.Button("Create"))
            {
                CreateEACompSci(symbolCount, strikeCount);
            }
        }

        public static void CreateEACompSci(int symbolCount, int strikeCount)
        {
            EncounterActionCompSci asset = ScriptableObject.CreateInstance<EncounterActionCompSci>();

            asset.SymbolCount = symbolCount;
            asset.StrikeCount = strikeCount;

            AssetDatabase.CreateAsset(asset, "Assets/Resources/EncounterActions/CompSciEA.asset");
            AssetDatabase.SaveAssets();

            Selection.activeObject = asset;
        }
    }
}
#endregion CompSci Encounter Action

#region Doctor Encounter Action
public class EncounterActionDoctor : ScriptableObject
{
    public int KeyStrokeCount;
    public float ArrowMinSpeed,
                 ArrowMaxSpeed,
                 SpawnRate;
}

public class MakeEncounterActionDoctor
{
    class EADoctorWindow : EditorWindow
    {
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
                CreateEADoctor(keyStrokeCount, arrowMinSpeed, arrowMaxSpeed, spawnRate);
            }
        }

        public static void CreateEADoctor(int keyStrokeCount, float arrowMinSpeed, float arrowMaxSpeed, float spawnRate)
        {
            EncounterActionDoctor asset = ScriptableObject.CreateInstance<EncounterActionDoctor>();

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
#endregion Doctor Encounter Action
#endregion Encounter Action Scriptable Objects