using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NPC : ScriptableObject
{
    public string NPCName;
    public int ConvoCount;
    //This needs to be a class; start here MASON
    public NextConvoData[] NextConvoInfo;
}

[System.Serializable]
public class NextConvoData
{
    public char MyIndex,
                NextIndex;
    public DialogChangeType MyChangeType;

    public NextConvoData(char myIndex, char nextIndex, DialogChangeType myChangeType)
    {
        MyIndex = myIndex;
        NextIndex = nextIndex;
        MyChangeType = myChangeType;
    }
}

#if UNITY_EDITOR
public class MakeNPCScritableObject
{
    class NPCWindow : EditorWindow
    {
        string npcName;
        int convoCount,
            transitionCount;
        NextConvoData[] nextConvoInfo;

        private string[] newIndexes = new string[12],
                         oldIndexes = new string[12];
        private DialogChangeType[] dctArray = new DialogChangeType[12];

        [MenuItem("Assets/Create/NPC")]
        public static void OpenNPCWindow()
        {
            EditorWindow.GetWindow(typeof(NPCWindow));
        }

        private void NextConvoBit(int i)
        {
            EditorGUILayout.LabelField("Next Convo Info");
            DialogChangeType dct = DialogChangeType.CONVOEND;

            oldIndexes[i] = EditorGUILayout.TextField("Convo ", oldIndexes[i]);
            newIndexes[i] = EditorGUILayout.TextField("Becomes convo ", newIndexes[i]);
            
            dctArray[i] = (DialogChangeType)EditorGUILayout.EnumPopup("Change Reason", dctArray[i]);

            if (newIndexes[i] == null || oldIndexes[i] == null)
                nextConvoInfo[i] = new NextConvoData('x', 'x', dct);
            else
                nextConvoInfo[i] = new NextConvoData(oldIndexes[i][0], newIndexes[i][0], dct);
        }

        private void OnGUI()
        {
            npcName = EditorGUILayout.TextField("NPC Name", npcName);
            convoCount = EditorGUILayout.IntField("Convo Count", convoCount);
            transitionCount = EditorGUILayout.IntField("Transition Count", transitionCount);
            EditorGUILayout.Space();

            if (transitionCount > 0)
            {
                nextConvoInfo = new NextConvoData[transitionCount];
            }
            else
            {
                nextConvoInfo = new NextConvoData[0];
            }

            if (transitionCount == 1)
            {
                NextConvoBit(0);
            }
            else if (transitionCount == 2)
            {
                #region 2 transitions
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                #endregion 2 transitions
            }
            else if (transitionCount == 3)
            {
                #region 3 transitions
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                #endregion 3 transitions
            }
            else if (transitionCount == 4)
            {
                #region 4 transitions
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                #endregion 4 transitions;
            }
            else if (transitionCount == 5)
            {
                #region 5 transitions
                NextConvoBit(0);
                EditorGUILayout.Space();
                NextConvoBit(1);
                EditorGUILayout.Space();
                NextConvoBit(2);
                EditorGUILayout.Space();
                NextConvoBit(3);
                EditorGUILayout.Space();
                NextConvoBit(4);
                #endregion 5 transitions
            }
            else if (transitionCount == 6)
            {
                #region 6 transitions
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
                #endregion 6 transitions
            }
            else if (transitionCount == 7)
            {
                #region 7 transitions
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
                #endregion 7 transitions
            }
            else if (transitionCount == 8)
            {
                #region 8 transitions
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
                #endregion 8 transitions
            }

			else if (transitionCount == 9)
			{
				#region 9 transitions
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
				EditorGUILayout.Space();
				NextConvoBit(8);
				#endregion 9 transitions
			}

			else if (transitionCount == 10)
			{
				#region 10 transitions
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
				EditorGUILayout.Space();
				NextConvoBit(8);
				EditorGUILayout.Space();
				NextConvoBit(9);
				#endregion 10 transitions
			}

            else if (transitionCount == 11)
            {
                #region 11 transitions
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
                EditorGUILayout.Space();
                NextConvoBit(8);
                EditorGUILayout.Space();
                NextConvoBit(9);
                EditorGUILayout.Space();
                NextConvoBit(10);
                #endregion 11 transitions
            }

            else if (transitionCount == 12)
            {
                #region 12 transitions
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
                EditorGUILayout.Space();
                NextConvoBit(8);
                EditorGUILayout.Space();
                NextConvoBit(9);
                EditorGUILayout.Space();
                NextConvoBit(10);
                EditorGUILayout.Space();
                NextConvoBit(11);
                #endregion 12 transitions
            }

            if (GUILayout.Button("Create"))
            {
                CreateNPC(npcName, convoCount, nextConvoInfo);
            }
        }

        public static void CreateNPC(string npcName, int convoCount, NextConvoData[] nextConvoInfo)
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
#endif