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

    #region GetFunctions
    public char GetNextEmotionType()
    {
        if(EmotionType == 'a')
        {
            return 'b';
        }
        else if(EmotionType == 'b')
        {
            return 'c';
        }
        else if (EmotionType == 'c')
        {
            return 'd';
        }
        else if (EmotionType == 'd')
        {
            return 'e';
        }
        else if (EmotionType == 'e')
        {
            return 'f';
        }
        else if (EmotionType == 'f')
        {
            return 'g';
        }
        else if (EmotionType == 'g')
        {
            return 'h';
        }
        else if (EmotionType == 'h')
        {
            return 'a';
        }
        else
        {
            Debug.LogError("That emotion type doesn't exist");
            return '?';
        }
    }

    public char GetLastEmotionType()
    {
        if (EmotionType == 'a')
        {
            return 'h';
        }
        else if (EmotionType == 'h')
        {
            return 'g';
        }
        else if (EmotionType == 'g')
        {
            return 'f';
        }
        else if (EmotionType == 'f')
        {
            return 'e';
        }
        else if (EmotionType == 'e')
        {
            return 'd';
        }
        else if (EmotionType == 'd')
        {
            return 'c';
        }
        else if (EmotionType == 'c')
        {
            return 'b';
        }
        else if (EmotionType == 'b')
        {
            return 'a';
        }
        else
        {
            Debug.LogError("That emotion type doesn't exist");
            return '?';
        }
    }
    #endregion
}