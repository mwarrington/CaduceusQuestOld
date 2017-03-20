using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPuzzleManager : MonoBehaviour
{
    public string Speaker,
                  SpeakerLine,
                  BadResponse,
                  MedResponse,
                  GoodResponse;
    public Emotion LineEmotion = new Emotion('g', 2);

    public void SelectEmotion(Emotion emotion)
    {
        if(emotion.EmotionType == LineEmotion.EmotionType)
        {
            if (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 1)
            {
                //Pefect
                Debug.Log("+5");
            }
            else if (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 2)
            {
                //Perfect type one off intensity
                Debug.Log("+3");
            }
            else if (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 3)
            {
                //Perfect type two off intensity
                Debug.Log("+1");
            }
            else
            {
                Debug.Log("-2");
            }
        }
        else if(emotion.EmotionType == LineEmotion.GetNextEmotionType() || emotion.EmotionType == LineEmotion.GetLastEmotionType())
        {
            if(Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 1)
            {
                //Pefect intensity wrong close type
                Debug.Log("+2");
            }
            else
            {
                Debug.Log("-" + (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) + 2));
            }
        }
        else
        {
            Debug.Log("-4");
        }
    }
}