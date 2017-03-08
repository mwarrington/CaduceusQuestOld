using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public string Name;

    void Update()
    {
        
    }
}

public class EncounterMenuItem
{
    public string ItemName;
    public List<EncounterMenuItem> SubMenuItems;
    public EncounterAction MyAction;

    //Contructors
    public EncounterMenuItem(string itemName)
    {
        ItemName = itemName;
    }

    public EncounterMenuItem(string itemName, List<EncounterMenuItem> subMenuItems)
    {
        ItemName = itemName;
        SubMenuItems = subMenuItems;
    }

    public EncounterMenuItem(string itemName, EncounterAction myAction)
    {
        ItemName = itemName;
        MyAction = myAction;
    }
}