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

        [MenuItem("Assets/Create/NPC")]
        public static void OpenNPCWindow()
        {
            EditorWindow.GetWindow(typeof(NPCWindow));
        }

        private void NextConvoBit(int i)
        {
            EditorGUILayout.LabelField("Next Convo Info");
            string index = "";
            DialogChangeType dct = DialogChangeType.CONVOEND;
            index = EditorGUILayout.TextField("Next Index", index);
            dct = (DialogChangeType)EditorGUILayout.EnumPopup("Change Reason", dct);
            Debug.Log("1: " + nextConvoInfo[i]);
            Debug.Log("2: " + index[0]);
            nextConvoInfo[i] = new KeyValuePair<char, DialogChangeType>(index[0], dct);
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
            //myDialogController = (DialogUIController)EditorGUILayout.EnumPopup()
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