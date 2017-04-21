using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextAsset[] TextAssets;
    private string _masterText;

    void Start()
    {
        Convorsation convo = new Convorsation("Bea", 'a');
    }

    public string GetMasterText()
    {
        for (int i = 0; i < TextAssets.Length; i++)
        {
            _masterText += "\n" + TextAssets[i].text;
        }

        return _masterText;
    }

    public string GetLine(int index)
    {
        return "Sup";
    }

    public string[] GetDialogOptions()
    {
        string[] newDOs = new string[3];
        return newDOs;
    }

    //This method makes a dialog Line from an existing dialog option
    //When this method is called and how exactly it works still requires some attention
    public Line MakeLineFromDO(DialogOption DO)
    {
        Line newLine = new Line();

        newLine.LineText = DO.DialogOptionText;
        newLine.MyEmotion = DO.DialogOptionEmotion;
        newLine.NextLineIndex = DO.MyNextLine;

        return newLine;
    }
}