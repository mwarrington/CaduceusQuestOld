﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public TextAsset MasterText;

    void Start()
    {
        Convorsation convo = new Convorsation("Patient", 'a');
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