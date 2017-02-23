using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public string Speaker,
                  LineText;
    public Emotion MyEmotion;
    public int DialogOptionsIndex,
               NextGroupIndex = -1,
               NextLineIndex = -1;
    public bool LastLine;
}