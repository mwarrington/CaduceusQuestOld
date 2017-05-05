using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{

	public string CharacterName;
	public char index;

	private DialogManager manager;
	private Convorsation convo;
	private GameObject _dialogueBox, 
		_dialogueEmotionBox,
		_dialoguePanel;


	private int _nextLineIndex = 0,
		_lastLine = 0,
		_currentDOIndex = 0;

	private Image _dialogueEmotionImage, 
		_dialogueBoxImage;

	private Button _option1, 
		_option2, 
		_option3,
		_option4;

	private Text _buttonText1, 
		_buttonText2, 
		_buttonText3,
		_buttonText4,
		_dialogueText;

	private bool _inConversation, 
		_dialogueSelection,
		_convoFinished;


	void Start ()
	{

		manager = GameObject.FindObjectOfType<DialogManager> (); 

		//Setting up Dialogue Option Buttons
		_option1 = GameObject.Find ("Option 1 Button").GetComponent<Button> ();
		_option2 = GameObject.Find ("Option 2 Button").GetComponent<Button> ();
		_option3 = GameObject.Find ("Option 3 Button").GetComponent<Button> ();
		//option4 = GameObject.Find ("Option 4 Button").GetComponent<Button> ();


		//Dialogue Option Text
		_buttonText1 = _option1.GetComponentInChildren<Text> ();
		_buttonText2 = _option2.GetComponentInChildren<Text> ();
		_buttonText3 = _option3.GetComponentInChildren<Text> ();
		//buttonText4 = option4.GetComponentInChildren<Text> ();

		//Dialogue Containers, Images and Text
		_dialoguePanel = GameObject.Find ("Dialogue Box Panel");
		_dialogueBox = GameObject.Find ("Dialogue Box Image");
		_dialogueEmotionBox = GameObject.Find ("Dialogue Emotion Image");

		_dialogueText = GameObject.Find ("Dialogue Text").GetComponent<Text> ();
		_dialogueEmotionImage = GameObject.Find ("Dialogue Emotion Image").GetComponent<Image> ();




		_dialoguePanel.SetActive (false);
		_option1.gameObject.SetActive (false);
		_option2.gameObject.SetActive (false);
		_option3.gameObject.SetActive (false);




		_inConversation = _dialogueSelection = false; 

		convo = new Convorsation (CharacterName, index);


		_lastLine = convo.MyLines.Count - 1;
		_convoFinished = false;
		_nextLineIndex = 0;


		Debug.Log (convo.Name + " has " + convo.MyLines.Count + " lines.");
		for (int i = 0; i < convo.MyLines.Count; i++) {
			Debug.Log (convo.Name + ": " + convo.MyLines [i].LineText);
		}

		Debug.Log ("Simone name has " + convo.MyDialogOptionsList.Count + " lines.");





		_option1.onClick.AddListener (delegate() {
			DialogueOptionSelected (0);
		});

		_option2.onClick.AddListener (delegate() {
			DialogueOptionSelected (1);
		});

		_option3.onClick.AddListener (delegate() {
			DialogueOptionSelected (3);

		});

		//		option4.onClick.AddListener (delegate() {
		//			DialogueOptionSelected (4);
		//		});
	}


	void Update ()
	{


		if (_inConversation) {

			if (Input.GetMouseButtonDown (0)) {

				if (_dialogueSelection) {
					ShowDialogueOptions ();
				} else if (convo.MyLines [_nextLineIndex].LastLine && _dialoguePanel.activeSelf) {
					_dialoguePanel.SetActive (false);
					_inConversation = false;
					_convoFinished = true;
				} else {
					_dialoguePanel.SetActive (true);
					GetNextLine ();
				}
			}
		}

	}


	void OnMouseOver ()
	{
		if (!_inConversation)
			this.gameObject.GetComponent<Renderer> ().material.color = Color.red;

	}





	void OnMouseExit ()
	{

		this.gameObject.GetComponent<Renderer> ().material.color = Color.white;
	}

	void OnMouseDown ()
	{
		if (!_inConversation) {
			_inConversation = true;
			_convoFinished = false;
		}


		if (_convoFinished) {
			_dialoguePanel.SetActive (true);

		}



	}



	private void ShowDialogueOptions ()
	{

		_dialoguePanel.SetActive (false);
		_option1.gameObject.SetActive (true);
		_option2.gameObject.SetActive (true);

		_buttonText1.text = convo.MyDialogOptionsList [_currentDOIndex].myOptions [0].DialogOptionText;
		_buttonText2.text = convo.MyDialogOptionsList [_currentDOIndex].myOptions [1].DialogOptionText;


	}

	private void HideDialogueOptions ()
	{

		_option1.gameObject.SetActive (false);
		_option2.gameObject.SetActive (false);
		_dialoguePanel.SetActive (true);

		_dialogueSelection = false;

		GetNextLine ();


	}



	private void GetNextLine ()
	{
		//		if (convo.MyLines[_nextLineIndex].LastLine) {
		//			_convoFinished = true;
		//
		//		
		//		}

		if (convo.MyLines [_nextLineIndex].LastLine) {

			_dialogueEmotionBox.GetComponent<Image> ().color = convo.MyLines [_lastLine].MyEmotion.GetEmotionColor ();
			_dialogueText.text = convo.MyLines [_lastLine].Speaker + ": " + convo.MyLines [_lastLine].LineText;


		} else if (_nextLineIndex < convo.MyLines.Count && !_dialogueSelection) {


			_dialogueEmotionBox.GetComponent<Image> ().color = convo.MyLines [_nextLineIndex].MyEmotion.GetEmotionColor ();
			_dialogueText.text = convo.MyLines [_nextLineIndex].Speaker + ": " + convo.MyLines [_nextLineIndex].LineText;


			if (!convo.MyLines [_nextLineIndex].LastLine) {

				if (convo.MyLines [_nextLineIndex].NextGroupIndex != -1) {

					_currentDOIndex = convo.MyLines [_nextLineIndex].NextGroupIndex;
					_dialogueSelection = true;
					Debug.Log ("Some Dialogue Options Here");
				}

				if (convo.MyLines [_nextLineIndex].NextLineIndex == -1) {
					_nextLineIndex++;
				} else {
					_nextLineIndex = convo.MyLines [_nextLineIndex].NextLineIndex;
				}


			}

		}

	}


	public void DialogueOptionSelected (int o)
	{

		_nextLineIndex = convo.MyDialogOptionsList [_currentDOIndex].myOptions [o].MyNextLine;
		HideDialogueOptions ();

	}
}
