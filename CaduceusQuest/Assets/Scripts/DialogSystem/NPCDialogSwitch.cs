using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogSwitch : MonoBehaviour
{
    public NPC NPCData;
    private BoxCollider _myTrigger;
    private DialogueUIController _theDialogController;
    private SimoneController _simone;
    private char _currentDialogIndex;
    private bool _inConvoZone,
                 _inConvorsation;

    private void Start()
    {
        _theDialogController = FindObjectOfType<DialogueUIController>();
        _simone = FindObjectOfType<SimoneController>();
        _myTrigger = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        if(_inConvoZone)
        {
            if(Input.GetKeyDown(KeyCode.Z) && !_inConvorsation)
            {
                _theDialogController.StartConversation(NPCData);
                _simone.Movement = false;
                _inConvorsation = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Simone")
        {
            _inConvoZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Simone")
        {
            _inConvoZone = false;
        }
    }
}