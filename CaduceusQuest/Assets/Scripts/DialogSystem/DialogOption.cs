using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOption
{
    public int MyNextLine;
    public string DialogOptionText;
    public Emotion DialogOptionEmotion;
}

public class DialogOptions
{
    public List<DialogOption> myOptions;

    public DialogOptions()
    {
        myOptions = new List<DialogOption>();
    }
}