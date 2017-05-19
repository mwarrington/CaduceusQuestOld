using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<DialogueUIController> _allDialogControllers = new List<DialogueUIController>();

    private void Start()
    {
        _allDialogControllers.AddRange(FindObjectsOfType<DialogueUIController>());
    }
}