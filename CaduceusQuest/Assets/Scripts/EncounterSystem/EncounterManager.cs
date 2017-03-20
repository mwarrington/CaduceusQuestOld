using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public string EncounterSubject;
    public float EncounterTrust;
    public List<EncounterController> EncounterControllers = new List<EncounterController>();

    private void CreateTurnOrder()
    {
        //EncounterControllers.Add(???)
    }
}