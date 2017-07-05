using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSelectionController : MonoBehaviour
{


	private string location;
	public GameObject selectionUI;
	public string sceneName;
	private NiceSceneTransition transition;

	// Use this for initialization
	void Awake ()
	{
		transition = GameObject.Find ("NiceSceneTransition").GetComponent<NiceSceneTransition> ();
	}

	void OnMouseEnter ()
	{

		selectionUI.SetActive (true);
	
	}


	void OnMouseExit ()
	{
		selectionUI.SetActive (false);

		
	}

	void OnMouseDown(){

		transition.LoadScene (sceneName);

	}


}
