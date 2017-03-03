using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEC : EncounterController
{
    public List<EncounterMenuItem> MyMenuItems = new List<EncounterMenuItem>();

    void Start()
    {
        //Instantiate my menu items
    }

    private void InstantiateMyMenuItems()
    {
        EncounterAction DefaultCompSciAction = new EncounterAction(EncounterActionType.COMPSCI, "Default");
        EncounterMenuItem compSci = new EncounterMenuItem("CompSci", DefaultCompSciAction);
    }
}