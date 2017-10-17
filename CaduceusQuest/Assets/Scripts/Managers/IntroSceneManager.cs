using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    public Image TutorialMask;
    public Canvas TutorialCanvas;
    public GameObject FirstButtonMessage;

    private bool _showingTutorial;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(!_showingTutorial)
            {
                _showingTutorial = true;
            }
            else
            {
                SceneManager.LoadScene("Hospital");
            }
        }

        if(_showingTutorial)
        {
            ShowTutorial();
        }
    }

    private void ShowTutorial()
    {
        if (TutorialMask.color.a < 1)
            TutorialMask.color = new Color(0, 0, 0, TutorialMask.color.a + Time.deltaTime);
        else
        {
            TutorialCanvas.enabled = true;
            FirstButtonMessage.SetActive(false);
        }
    }
}