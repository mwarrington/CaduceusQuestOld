using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour
{
	#region initialization

	public string CharacterName;
	public char index;

	private BoxCollider _myTrigger;
	private DialogManager _dialogueManager;
	private SimoneController simone;
	private Convorsation convo;
	private GameObject _dialogueBox,
		_dialogueEmotionBox,
		_dialoguePanel;

	private int _nextLineIndex = 0,
		_lastLine = 0,
		_currentDOIndex = 0,
		_buttonSelectionIndex = 0;

	private Image _dialogueEmotionImage,
		_dialogueBoxImage;

	private Sprite _buttonNormalSprite,
		_buttonHighlightedSprite;

	private Button _option1,
		_option2,
		_option3,
		_option4;

	private ColorBlock _highlightButtonColor,
		_normalButtonColor;

	private Text _buttonText1,
		_buttonText2,
		_buttonText3,
		_buttonText4,
		_dialogueText,
		_simoneNameText1,
		_simoneNameText2,
		_simoneNameText3,
		_speakerNameText;

	private bool _inConversation,
		_dialogueSelection,
		_convoFinished,
		_inConvoZone,
		_isWriting;

	private List<Button> _optionButtonList = new List<Button>();

	private List<Text> _optionTextList = new List<Text>(),
		_optionNameList = new List<Text>();

	#endregion

	void Start()
	{
		_myTrigger = GetComponentInChildren<BoxCollider>();
		_dialogueManager = GameObject.FindObjectOfType<DialogManager>();
		simone = GameObject.FindObjectOfType<SimoneController>();

		//Setting up Dialogue Option Buttons
		_option1 = GameObject.Find("Option 1 Button").GetComponent<Button>();
		_option2 = GameObject.Find("Option 2 Button").GetComponent<Button>();
		_option3 = GameObject.Find("Option 3 Button").GetComponent<Button>();
		//option4 = GameObject.Find ("Option 4 Button").GetComponent<Button> ();

		//Setting up Dialogue Button Colors
		_buttonHighlightedSprite = _option1.spriteState.highlightedSprite;
		_buttonNormalSprite = _option1.GetComponent<Image>().sprite;

		//Add option buttons to list
		_optionButtonList.Add(_option1);
		_optionButtonList.Add(_option2);
		_optionButtonList.Add(_option3);

		//Dialogue Option Text
		_buttonText1 = _option1.transform.Find("Text").GetComponent<Text>();
		_buttonText2 = _option2.transform.Find("Text").GetComponent<Text>();
		_buttonText3 = _option3.transform.Find("Text").GetComponent<Text>();

		//Add option text to list
		_optionTextList.Add(_buttonText1);
		_optionTextList.Add(_buttonText2);
		_optionTextList.Add(_buttonText3);

		//Simone name text
		_simoneNameText1 = _option1.transform.FindChild("Simone Name Text").GetComponent<Text>();
		_simoneNameText2 = _option2.transform.FindChild("Simone Name Text").GetComponent<Text>();
		_simoneNameText3 = _option3.transform.FindChild("Simone Name Text").GetComponent<Text>();

		//Add simone name text to list
		_optionNameList.Add(_simoneNameText1);
		_optionNameList.Add(_simoneNameText2);
		_optionNameList.Add(_simoneNameText3);

		//Dialogue Containers, Images and Text
		_dialoguePanel = GameObject.Find("Dialogue Box Panel");
		_dialogueBox = GameObject.Find("Dialogue Box Image");
		_dialogueEmotionBox = GameObject.Find("Dialogue Emotion Image");
		_dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
		_dialogueEmotionImage = GameObject.Find("Dialogue Emotion Image").GetComponent<Image>();
		_speakerNameText = GameObject.Find("Speaker Name Text").GetComponent<Text>();

		_dialoguePanel.SetActive(false);
		_option1.gameObject.SetActive(false);
		_option2.gameObject.SetActive(false);
		_option3.gameObject.SetActive(false);

		_inConversation = _dialogueSelection = false;

		convo = new Convorsation(CharacterName, index);

		_lastLine = convo.MyLines.Count - 1;
		_convoFinished = false;
		_nextLineIndex = 0;

	}

	void Update()
	{

		if (_inConvoZone)
		{

			if (Input.GetKeyDown(KeyCode.Z))
			{
				if (!_inConversation)
				{
					_inConversation = true;
					_convoFinished = false;
					simone.Movement = false;
				}

				if (_convoFinished)
				{
					_dialoguePanel.SetActive(true);
					_convoFinished = true;
					simone.Movement = true;
			
				}

				StartConversation();
			}

		}
		else
		{

			_dialoguePanel.SetActive(false);

		}

		if (_dialogueSelection)
		{

			if (_buttonSelectionIndex < 0)
			{
				_buttonSelectionIndex = 0;
				HighlightDialogueSelection();
			}
			else if (_buttonSelectionIndex >= convo.MyDialogOptionsList[_currentDOIndex].myOptions.Count - 1)
			{
				_buttonSelectionIndex = convo.MyDialogOptionsList[_currentDOIndex].myOptions.Count - 1;
				HighlightDialogueSelection();
			}

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_buttonSelectionIndex--;
				HighlightDialogueSelection();
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				_buttonSelectionIndex++;
				HighlightDialogueSelection();
			}

			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				DialogueOptionSelected(_buttonSelectionIndex);
			}
		}


	}

	private void StartConversation()
	{
		if (_dialogueSelection)
		{
			ShowDialogueOptions();
		}
		else if (convo.MyLines[_nextLineIndex].LastLine && _dialoguePanel.activeSelf)
		{
			_dialoguePanel.SetActive(false);
			_inConversation = false;
			_convoFinished = true;
			simone.Movement = true;
		}
		else if (!_inConversation && _dialoguePanel.activeSelf && _inConvoZone)
		{
			_dialoguePanel.SetActive(false);
			simone.Movement = true;
		}
		else
		{
			_dialoguePanel.SetActive(true);
			GetNextLine();
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

	private void ShowDialogueOptions()
	{
		_dialoguePanel.SetActive(false);

		for (int i = 0; i < convo.MyDialogOptionsList[_currentDOIndex].myOptions.Count; i++)
		{

			_optionButtonList[i].gameObject.SetActive(true);
			_optionTextList[i].text = convo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionText;

		}

		HighlightDialogueSelection();

	}

	private void HideDialogueOptions()
	{

		foreach (Button b in _optionButtonList)
		{
			b.gameObject.SetActive(false);
		}

		_dialoguePanel.SetActive(true);
		_dialogueSelection = false;

		GetNextLine();

	}

	private void GetNextLine()
	{

		if (convo.MyLines[_nextLineIndex].LastLine)
		{

			WriteDialogue();
		}
		else if (!convo.MyLines[_nextLineIndex].LastLine && !_dialogueSelection)
		{
			WriteDialogue();

			if (convo.MyLines[_nextLineIndex].NextGroupIndex != -1)
			{

				_currentDOIndex = convo.MyLines[_nextLineIndex].NextGroupIndex;
				_dialogueSelection = true;

			}
			_nextLineIndex++;
		}
		else
		{
			_nextLineIndex = convo.MyLines[_nextLineIndex].NextLineIndex;
		}

	}

	public void DialogueOptionSelected(int o)
	{
		_nextLineIndex = convo.MyDialogOptionsList[_currentDOIndex].myOptions[o].MyNextLine;
		HideDialogueOptions();

	}

	private void HighlightDialogueSelection()
	{

		for (int i = 0; i < convo.MyDialogOptionsList[_currentDOIndex].myOptions.Count; i++)
		{
			if (i == _buttonSelectionIndex)
			{

				_optionButtonList[_buttonSelectionIndex].GetComponent<Image>().sprite = _buttonHighlightedSprite;
				_optionTextList[_buttonSelectionIndex].color = Color.black;
				_optionNameList[_buttonSelectionIndex].color = Color.black;

			}
			else
			{
				_optionButtonList[i].GetComponent<Image>().sprite = _buttonNormalSprite;
				_optionTextList[i].color = Color.white;
				_optionNameList[i].color = Color.white;

			}

		}
	}

	private void WriteDialogue()
	{
		_isWriting = true;
		_dialogueEmotionBox.GetComponent<Image>().color = convo.MyLines[_nextLineIndex].MyEmotion.GetEmotionColor();
		_speakerNameText.text = convo.MyLines[_nextLineIndex].Speaker.ToUpper() + ":";
		_dialogueText.text = "";

		StartCoroutine(TypeText());
	}

	IEnumerator TypeText()
	{
		foreach (char c in convo.MyLines[_nextLineIndex].LineText.ToCharArray())
		{
			_dialogueText.text += c;
			yield return new WaitForSeconds(_dialogueManager.TypeSpeed);
		}
		_isWriting = false;
	}
}

