using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPuzzleManager : MonoBehaviour
{
    public string Name,
                  Speaker,
                  SpeakerLine,
                  BadResponse,
                  GoodResponse;
    public float FailPenalty;
    public Emotion LineEmotion = new Emotion('g', 2);

    private EncounterManager _theEncounterMan;

    private void Start()
    {
        _theEncounterMan = FindObjectOfType<EncounterManager>();
    }

    public void SelectEmotion(Emotion emotion)
    {
        if(emotion.EmotionType == LineEmotion.EmotionType)
        {
            if (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 1)
            {
                //Pefect
                _theEncounterMan.PuzzleWin(FailPenalty, this.gameObject);
            }
            else if (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 2)
            {
                //Perfect type one off intensity
                _theEncounterMan.PuzzleWin(FailPenalty, this.gameObject);
            }
            else if (Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 3)
            {
                //Perfect type two off intensity
                _theEncounterMan.PuzzleFail(FailPenalty, this.gameObject, EncounterActionType.DIALOG);
            }
            else
            {
                _theEncounterMan.PuzzleFail(FailPenalty, this.gameObject, EncounterActionType.DIALOG);
            }
        }
        else if(emotion.EmotionType == LineEmotion.GetNextEmotionType() || emotion.EmotionType == LineEmotion.GetLastEmotionType())
        {
            if(Mathf.Abs(emotion.EmotionIntensity - LineEmotion.EmotionIntensity) < 1)
            {
                //Pefect intensity wrong close type
                _theEncounterMan.PuzzleFail(FailPenalty, this.gameObject, EncounterActionType.DIALOG);
            }
            else
            {
                _theEncounterMan.PuzzleFail(FailPenalty, this.gameObject, EncounterActionType.DIALOG);
            }
        }
        else
        {
            _theEncounterMan.PuzzleFail(FailPenalty, this.gameObject, EncounterActionType.DIALOG);
        }
    }
}