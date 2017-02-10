using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion
{
    public char EmotionType;
    public int EmotionIntensity;

    public Emotion(char type, int intensity)
    {
        EmotionType = type;
        EmotionIntensity = intensity;
    }
}