﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextAsset[] TextAssets;
    private string _masterText;

    void Start()
    {
        Convorsation convo = new Convorsation("Patient", 'a');
    }

    public string GetMasterText()
    {
        string masterText = "";

        for (int i = 0; i < TextAssets.Length; i++)
        {
            _masterText += "\n" + TextAssets[i].text;
        }

        return masterText;
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
}