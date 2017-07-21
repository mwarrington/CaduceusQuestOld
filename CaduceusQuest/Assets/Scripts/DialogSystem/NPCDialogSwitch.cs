using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogSwitch : MonoBehaviour
{
    public NPC NPCData;
    private GameObject _simone;
    private BoxCollider _myTrigger;
    private DialogueUIController _theDialogController;
    private char _currentDialogIndex;
    private bool _inConvoZone,
                 _inConvorsation,
                 _convoPrimed;

    private void Start()
    {
        _theDialogController = FindObjectOfType<DialogueUIController>();
        _myTrigger = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        if(_inConvoZone && !_inConvorsation)
        {
            if(Input.GetKeyDown(KeyCode.Z) && !_inConvorsation)
            {
                _convoPrimed = true;
            }

            if (Input.GetKeyUp(KeyCode.Z) && _convoPrimed && !_inConvorsation)
            {
                _theDialogController.StartConversation(NPCData, this);
                _inConvorsation = true;
                _convoPrimed = false;
                _simone.transform.LookAt(new Vector3(this.transform.position.x, _simone.transform.position.y, this.transform.position.z), _simone.transform.up);
            }
        }
        else
        {
            _convoPrimed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Simone")
        {
            if (_simone == null)
                _simone = other.gameObject;

            _inConvoZone = true;
            _theDialogController.ToggleInteractObj(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Simone")
        {
            _inConvoZone = false;
            _theDialogController.ToggleInteractObj(false);
        }
    }

    public void ExitDialog()
    {
        _inConvorsation = false;
        _convoPrimed = false;
    }
}