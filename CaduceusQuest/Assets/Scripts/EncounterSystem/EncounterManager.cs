using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public string EncounterSubject;
    public List<EncounterController> EncounterControllers = new List<EncounterController>();
    public List<IndividualGoal> EncounterGoals;

    private void CreateTurnOrder()
    {
        //EncounterControllers.Add(???)
    }
}

public class IndividualGoal
{
    public EncounterActionType MyEAType;
    public string EAName;
    public float IndividualTrust;
    public int ActionCountGoal;

    public IndividualGoal(int actionCountGoal, float individualTrust, string eaName, EncounterActionType myEAType)
    {
        MyEAType = myEAType;
        EAName = eaName;
        IndividualTrust = individualTrust;
        ActionCountGoal = actionCountGoal;
    }
}