using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGoal> EncounterGoals = new List<EncounterGoal>();
    public List<EncounterController> EncounterControllers = new List<EncounterController>();

    //public GameObject[] SubMenus;

    private void CreateTurnOrder()
    {
        //EncounterControllers.Add(???)
    }

    private void OnGUI()
    {
        UIHandler();
    }

    private void UIHandler()
    {

    }

    public void ShowSubMenu(GameObject subMenu)
    {

    }
}