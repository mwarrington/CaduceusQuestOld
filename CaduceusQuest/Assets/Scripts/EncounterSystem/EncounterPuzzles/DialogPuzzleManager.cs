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
    public Emotion LineEmotion = new Emotion('a', 0);


}