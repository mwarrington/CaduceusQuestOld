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
        //This is gonna be tricky...
        EncounterMenuItem compSci = new EncounterMenuItem("CompSci");
    }
}