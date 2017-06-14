using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour
{
    #region initialization
    private GameManager _theGameManager;
	private DialogManager _dialogueManager;
    private SimoneController _simone;
    private Convorsation _currentConvo;
    private IEnumerator _currentCoroutine;
	private GameObject _dialogueBox,
		_dialogueEmotionBox,
		_dialoguePanel;

	private int _currentLineIndex = 0,
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
		_isWriting,
        _lastLine;

	private List<Button> _optionButtonList = new List<Button>();

	private List<Text> _optionTextList = new List<Text>(),
		_optionNameList = new List<Text>();

	#endregion

	void Start()
	{
        _theGameManager = FindObjectOfType<GameManager>();
		_dialogueManager = GameObject.FindObjectOfType<DialogManager>();

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
		_simoneNameText1 = _option1.transform.Find("Simone Name Text").GetComponent<Text>();
		_simoneNameText2 = _option2.transform.Find("Simone Name Text").GetComponent<Text>();
		_simoneNameText3 = _option3.transform.Find("Simone Name Text").GetComponent<Text>();

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

        _simone = FindObjectOfType<SimoneController>();

        _inConversation = _dialogueSelection = false;

		_convoFinished = false;
	}

	void Update()
	{
        if(_inConversation)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                if(_isWriting)
                {
                    StopCoroutine(_currentCoroutine);
                    _dialogueText.text = _currentConvo.MyLines[_currentLineIndex].LineText;
                    _isWriting = false;
                }
                else if (_lastLine)
                {
                    _dialoguePanel.SetActive(false);
                    _inConversation = false;
                    _convoFinished = true;
                    _simone.Movement = true;
                }
                else if (!_isWriting)
                    GetNextLine();
            }
        }

		if (_dialogueSelection)
		{
			if (_buttonSelectionIndex < 0)
			{
				_buttonSelectionIndex = 0;
				HighlightDialogueSelection();
			}
			else if (_buttonSelectionIndex >= _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count - 1)
			{
				_buttonSelectionIndex = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count - 1;
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
				_currentLineIndex = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[_buttonSelectionIndex].MyNextLine;
				HideDialogueOptions();
			}
		}
	}

	public void StartConversation(NPC npc)
	{
        _currentConvo = new Convorsation(npc.NPCName, _theGameManager.CurrentDialogIndexList[npc.NPCName]);
        _inConversation = true;
        _dialoguePanel.SetActive(true);
        _simone.Movement = false;
        GetNextLine();
	}

	private void ShowDialogueOptions()
	{
		_dialoguePanel.SetActive(false);

		for (int i = 0; i < _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count; i++)
		{
			
			_optionButtonList[i].gameObject.SetActive(true);
			_optionTextList[i].text = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionText.Trim();

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
		if (_currentConvo.MyLines[_currentLineIndex].LastLine)
		{
            _lastLine = true;
			WriteDialogue();
		}
		else if (!_dialogueSelection)
		{
			WriteDialogue();

			if (_currentConvo.MyLines[_currentLineIndex].NextGroupIndex != -1)
			{
				_currentDOIndex = _currentConvo.MyLines[_currentLineIndex].NextGroupIndex;
				_dialogueSelection = true;
			}

            _currentLineIndex++;
		}
		else
		{
            Debug.Log("YOU WERE WRONG, MASON!!!!");
            _currentLineIndex = _currentConvo.MyLines[_currentLineIndex].NextLineIndex;
		}

	}

	private void HighlightDialogueSelection()
	{
		for (int i = 0; i < _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count; i++)
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
		_dialogueEmotionBox.GetComponent<Image>().color = _currentConvo.MyLines[_currentLineIndex].MyEmotion.GetEmotionColor();
		_speakerNameText.text = _currentConvo.MyLines[_currentLineIndex].Speaker.ToUpper() + ":";
		_dialogueText.text = "";

        _currentCoroutine = TypeText();
		StartCoroutine(_currentCoroutine);
	}

	IEnumerator TypeText()
	{
		foreach (char c in _currentConvo.MyLines[_currentLineIndex].LineText.ToCharArray())
		{
			_dialogueText.text += c;
			yield return new WaitForSeconds(_dialogueManager.TypeSpeed);
		}
		_isWriting = false;
	}

    private void DialogueChanger()
    {
        //Start here
    }
}

