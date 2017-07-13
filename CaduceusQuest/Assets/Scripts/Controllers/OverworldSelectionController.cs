using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSelectionController : MonoBehaviour
{
    public List<GameObject> UISelections = new List<GameObject>();
    public string sceneName;

    private NiceSceneTransition transition;
    private string location;
    private int currentUISelection;

    void Awake()
    {
        transition = GameObject.Find("NiceSceneTransition").GetComponent<NiceSceneTransition>();
        UpdateMenu();
    }

    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentUISelection--;
            if (currentUISelection < 0)
                currentUISelection = UISelections.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentUISelection++;
            if (currentUISelection == UISelections.Count)
                currentUISelection = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            switch(currentUISelection )
            {
                case 0:
                    transition.LoadScene("Hospital");
                    break;
                case 1:
                    transition.LoadScene("School1");
                    break;
                default:
                    Debug.LogError("We don't have that many locations...");
                    break;
            }
        }

        UpdateMenu();
    }

    void OnMouseDown()
    {
        transition.LoadScene(sceneName);
    }
    
    private void UpdateMenu()
    {
        for (int i = 0; i < UISelections.Count; i++)
        {
            if (i == currentUISelection)
                UISelections[i].SetActive(true);
            else
                UISelections[i].SetActive(false);
        }
    }
}
