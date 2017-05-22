using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneController : MonoBehaviour
{
    
}

public class NPC : ScriptableObject
{
    public string NPCName;
    public int ConvoCount;
    public KeyValuePair<char, DialogChangeType>[] NextConvoInfo;
}

public class MakeNPCScritableObject
{
    class NPCWindow : EditorWindow
    {
        string npcName;
        int convoCount;
        KeyValuePair<char, DialogChangeType>[] nextConvoInfo;

        private string[] indexes = new string[8];
        private DialogChangeType[] dctArray = new DialogChangeType[8];

        [MenuItem("Assets/Create/NPC")]
        public static void OpenNPCWindow()
        {
            EditorWindow.GetWindow(typeof(NPCWindow));
        }

        private void NextConvoBit(int i)
        {
            EditorGUILayout.LabelField("Next Convo Info");
            DialogChangeType dct = DialogChangeType.CONVOEND;
            switch(i)
            {
                case 0:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo a", indexes[i]);
                    break;
                case 1:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo b", indexes[i]);
                    break;
                case 2:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo c", indexes[i]);
                    break;
                case 3:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo d", indexes[i]);
                    break;
                case 4:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo e", indexes[i]);
                    break;
                case 5:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo f", indexes[i]);
                    break;
                case 6:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo g", indexes[i]);
                    break;
                case 7:
                    indexes[i] = EditorGUILayout.TextField("Next convo for convo h", indexes[i]);
                    break;
                default:
                    Debug.LogError("That is too many convos...");
                    break;
            }

            dctArray[i] = (DialogChangeType)EditorGUILayout.EnumPopup("Change Reason", dctArray[i]);

            if (indexes[i] == null)
                nextConvoInfo[i] = new KeyValuePair<char, DialogChangeType>('x', dct);
            else
                nextConvoInfo[i] = new KeyValuePair<char, DialogChangeType>(indexes[0][0], dct);
        }

        private void OnGUI()
        {
            npcName = EditorGUILayout.TextField("NPC Name", npcName);
            convoCount = EditorGUILayout.IntField("ConvoCount", convoCount);
            EditorGUILayout.Space();

            if (convoCount > 1)
            {
                nextConvoInfo = new KeyValuePair<char, DialogChangeType>[convoCount - 1];
            }
            else
            {
                nextConvoInfo = new KeyValuePair<char, DialogChangeType>[0];
            }

            if (convoCount == 2)
            {
                NextConvoBit(0);
            }
            else if (convoCount == 3)
            {
                #region 3 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                #endregion 3 convos
            }
            else if (convoCount == 4)
            {
                #region 4 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                #endregion 4 convos
            }
            else if (convoCount == 5)
            {
                #region 5 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                #endregion 5 convos;
            }
            else if (convoCount == 6)
            {
                #region 6 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                EditorGUILayout.Space();
                NextConvoBit(4);
                #endregion 6 convos
            }
            else if (convoCount == 7)
            {
                #region 7 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                EditorGUILayout.Space();
                NextConvoBit(4);
                EditorGUILayout.Space();
                NextConvoBit(5);
                #endregion 7 convos
            }
            else if (convoCount == 8)
            {
                #region 8 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                EditorGUILayout.Space();
                NextConvoBit(4);
                EditorGUILayout.Space();
                NextConvoBit(5);
                EditorGUILayout.Space();
                NextConvoBit(6);
                #endregion 8 convos
            }
            else if (convoCount == 9)
            {
                #region 9 convos
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                EditorGUILayout.Space();
                NextConvoBit(4);
                EditorGUILayout.Space();
                NextConvoBit(5);
                EditorGUILayout.Space();
                NextConvoBit(6);
                EditorGUILayout.Space();
                NextConvoBit(7);
                #endregion 9 convos
            }

            if (GUILayout.Button("Create"))
            {
                CreateNPC(npcName, convoCount, nextConvoInfo);
            }
        }

        public static void CreateNPC(string npcName, int convoCount, KeyValuePair<char, DialogChangeType>[] nextConvoInfo)
        {
            NPC theNPC = ScriptableObject.CreateInstance<NPC>();

            if (convoCount > 1)
            {
                theNPC.NextConvoInfo = nextConvoInfo;
            }

            theNPC.NPCName = npcName;
            theNPC.ConvoCount = convoCount;

            AssetDatabase.CreateAsset(theNPC, "Assets/Resources/SceneData/NewNPC.asset");
            AssetDatabase.SaveAssets();

            Selection.activeObject = theNPC;
        }
    }
}