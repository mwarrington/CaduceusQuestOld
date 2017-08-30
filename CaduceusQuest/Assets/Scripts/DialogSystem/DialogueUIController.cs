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
		               _dialoguePanel,
                       _interactObject,
                       _eventMessageObject;
    private DialogChangeType _currentChangeType;
    private NPCDialogSwitch _currentDialogSwitch;

    private char _nextConvo;

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
		_speakerNameText,
        _eventMessageText;

	private bool _inConversation,
        _optionsNext,
		_dialogueSelection,
		_convoFinished,
		_isWriting,
        _beginEncounter,
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
		//_dialogueEmotionBox = GameObject.Find("Dialogue Emotion Image");
		_dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
		_speakerNameText = GameObject.Find("Speaker Name Text").GetComponent<Text>();
        _dialogueEmotionImage = _dialogueBox.GetComponent<Image>();

		_dialoguePanel.SetActive(false);
		_option1.gameObject.SetActive(false);
		_option2.gameObject.SetActive(false);
		_option3.gameObject.SetActive(false);

        _interactObject = GameObject.Find("Button Press Object");
        _interactObject.SetActive(false);

        _eventMessageText = GameObject.Find("EventMessageImage/Text").GetComponent<Text>();
        _eventMessageObject = GameObject.Find("Event Message Panel");
        _eventMessageObject.SetActive(false);

        _simone = FindObjectOfType<SimoneController>();

        _inConversation = _dialogueSelection = false;

		_convoFinished = false;
    }

    public void StartConversation(NPC npc, NPCDialogSwitch currentDialogSwitch)
    {
        _currentLineIndex = 0;
        _currentDOIndex = 0;
        _buttonSelectionIndex = 0;
        _lastLine = false;
        _beginEncounter = false;
        _currentConvo = new Convorsation(npc.NPCName, _theGameManager.CurrentDialogIndexList[npc.NPCName]);
        _inConversation = true;
        _currentDialogSwitch = currentDialogSwitch;
        _dialoguePanel.SetActive(true);
        int nextConvoInfoIndex = GetIndexFromChar(_currentConvo.Index);
        if (nextConvoInfoIndex >= npc.NextConvoInfo.Length)
        {
            _nextConvo = '!';
        }
        else
        {
            _currentChangeType = npc.NextConvoInfo[nextConvoInfoIndex].MyChangeType;
            _nextConvo = npc.NextConvoInfo[nextConvoInfoIndex].NextIndex;
        }
        _simone.Movement = false;
        GetNextLine();
    }

    void Update()
	{
        if (_dialogueSelection)
        {
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

            if (_buttonSelectionIndex < 0)
            {
                _buttonSelectionIndex = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count - 1;
                HighlightDialogueSelection();
            }
            else if (_buttonSelectionIndex >= _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count)
            {
                _buttonSelectionIndex = 0;
                HighlightDialogueSelection();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Z))
            {
                _currentLineIndex = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[_buttonSelectionIndex].MyNextLine;
                _inConversation = true;
                HideDialogueOptions();
            }
        }
        else if (_inConversation)
        {
            if(Input.GetKeyUp(KeyCode.Z))
            {
                if(_isWriting)
                {
                    StopCoroutine(_currentCoroutine);
                    _dialogueText.text = "";

                    for (int i = 0; i < _currentConvo.MyLines[_currentLineIndex].LineText.Length; i++)
                    {
                        if (i != 0 && i != (_currentConvo.MyLines[_currentLineIndex].LineText.Length - 1))
                        {
                            _dialogueText.text += _currentConvo.MyLines[_currentLineIndex].LineText[i];
                        }
                    }
                    
                    _isWriting = false;
                    if (!_lastLine)
                        _currentLineIndex++;
                }
                else if (_beginEncounter)
                {
                    string path = "EncounterData/" + _currentConvo.SpeakerName + "Encounter" + _currentConvo.MyLines[_currentLineIndex].EncounterToStart;
                    _theGameManager.DialogueChanger(_currentConvo.SpeakerName, DialogChangeType.CONVOEND);
                    _theGameManager.LastPosition = _simone.transform.position;
                    _theGameManager.BeginEncounter(path);
                }
                else if (_lastLine)
                {
                    _dialoguePanel.SetActive(false);
                    _inConversation = false;
                    _convoFinished = true;
                    _simone.Movement = true;
                    _currentDialogSwitch.ExitDialog();

                    //HACK: I'm gonna have to make a more fleshed out event system
                    if (_currentConvo.SpeakerName == "Dr. Gallo" && _currentConvo.Index == 'a')
                    {
                        string[] names = new string[3] { "Sylvia", "Mason", "Violet" };
                        _theGameManager.DialogueChanger(_currentConvo.SpeakerName, DialogChangeType.CONVOEND, names);
                    }
                    else if(_currentConvo.SpeakerName == "Dr. Gallo" && _currentConvo.Index == 'c')
                    {
                        string[] names = new string[1] { "Sylvia" };
                        _theGameManager.DialogueChanger(_currentConvo.SpeakerName, DialogChangeType.CONVOEND, names);
                    }
                    else
                    {
                        _theGameManager.DialogueChanger(_currentConvo.SpeakerName, DialogChangeType.CONVOEND);
                    }
                }
                else if (!_isWriting)
                    GetNextLine();
            }
        }
	}

    private int GetIndexFromChar(char indexChar)
    {
        int indexToGive = -1;

        switch(indexChar)
        {
            case 'a':
                indexToGive = 0;
                break;
            case 'b':
                indexToGive = 1;
                break;
            case 'c':
                indexToGive = 2;
                break;
            case 'd':
                indexToGive = 3;
                break;
            case 'e':
                indexToGive = 4;
                break;
            case 'f':
                indexToGive = 5;
                break;
            case 'g':
                indexToGive = 6;
                break;
            case 'h':
                indexToGive = 7;
                break;
            case 'i':
                indexToGive = 8;
                break;
            case 'j':
                indexToGive = 9;
                break;
            case 'k':
                indexToGive = 10;
                break;
            case 'l':
                indexToGive = 11;
                break;
            case 'm':
                indexToGive = 12;
                break;
            case 'n':
                indexToGive = 13;
                break;
            case 'o':
                indexToGive = 14;
                break;
            case 'p':
                indexToGive = 15;
                break;
            case 'q':
                indexToGive = 16;
                break;
            case 'r':
                indexToGive = 17;
                break;
            case 's':
                indexToGive = 18;
                break;
            case 't':
                indexToGive = 19;
                break;
            case 'u':
                indexToGive = 20;
                break;
            case 'v':
                indexToGive = 21;
                break;
            case 'w':
                indexToGive = 22;
                break;
            case 'x':
                indexToGive = 23;
                break;
            case 'y':
                indexToGive = 24;
                break;
            case 'z':
                indexToGive = 25;
                break;
            default:
                indexToGive = -1;
                Debug.Log("I don't think we can expand this switch...");
                break;
        }

        return indexToGive;
    }

	private void ShowDialogueOptions()
	{
		_dialoguePanel.SetActive(false);

		for (int i = 0; i < _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count; i++)
		{
			_optionButtonList[i].gameObject.SetActive(true);
            _optionTextList[i].text = "";

            for (int j = 0; j < _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionText.Length; j++) //char c in _currentConvo.MyLines[_currentLineIndex].LineText.ToCharArray())
            {
                if (j != 0 && j != (_currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionText.Length - 1))
                {
                    _optionTextList[i].text += _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionText[j];
                }
            }

            //_optionTextList[i].text = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionText;
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
        _optionsNext = false;
        _dialogueSelection = false;

		GetNextLine();
	}

	private void GetNextLine()
	{
        if(_optionsNext)
        {
            ShowDialogueOptions();
            _inConversation = false;
            _dialogueSelection = true;
        }
		else if (_currentConvo.MyLines[_currentLineIndex].LastLine)
		{
            _lastLine = true;

            if (_currentConvo.MyLines[_currentLineIndex].BeginEncounter)
            {
                _beginEncounter = true;
            }

            WriteDialogue();
		}
		else
		{
            if (_currentConvo.MyLines[_currentLineIndex].NextGroupIndex != -1)
            {
                _currentDOIndex = _currentConvo.MyLines[_currentLineIndex].NextGroupIndex;
                _optionsNext = true;
            }

            WriteDialogue();
        }

    }

	private void HighlightDialogueSelection()
	{
		for (int i = 0; i < _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions.Count; i++)
		{
			if (i == _buttonSelectionIndex)
			{
                Image currentButtonImage = _optionButtonList[i].GetComponent<Image>();

                currentButtonImage.sprite = _buttonHighlightedSprite;
                currentButtonImage.color = _currentConvo.MyDialogOptionsList[_currentDOIndex].myOptions[i].DialogOptionEmotion.GetEmotionColor();

                _optionTextList[i].color = Color.black;
                _optionNameList[i].color = Color.black;
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
		_dialogueEmotionImage.color = _currentConvo.MyLines[_currentLineIndex].MyEmotion.GetEmotionColor();
        string speakerName = _currentConvo.MyLines[_currentLineIndex].Speaker.ToUpper();
        string trueName = "";
        for (int i = 0; i < speakerName.Length; i++)
        {
            if (speakerName[i] != '0' && speakerName[i] != '1' && speakerName[i] != '2' && speakerName[i] != '3' && speakerName[i] != '4' &&
                speakerName[i] != '5' && speakerName[i] != '6' && speakerName[i] != '7' && speakerName[i] != '8' && speakerName[i] != '9')
            {
                trueName += speakerName[i];
            }
            else
                break;
        }
        _speakerNameText.text = trueName + ":";
		_dialogueText.text = "";

        _currentCoroutine = TypeText();
		StartCoroutine(_currentCoroutine);
	}

    public void ToggleInteractObj(bool onOff)
    {
        _interactObject.SetActive(onOff);
    }

    public void ShowEventMessage(string eventGoal)
    {
        _eventMessageObject.SetActive(true);
        
        switch(eventGoal)
        {
            case "Complete Intake Form":
                _eventMessageText.text = "Alright! You've completed all Intake Forms!";
                break;
            case "Collect Blood Sample":
                _eventMessageText.text = "Good Job! You've collected all blood samples!";
                break;
            default:
                break;
        }

        Invoke("HideEventMessage", 3);
    }

    private void HideEventMessage()
    {
        _eventMessageObject.SetActive(false);
    }

    IEnumerator TypeText()
	{
		for (int i = 0; i < _currentConvo.MyLines[_currentLineIndex].LineText.Length; i++) //char c in _currentConvo.MyLines[_currentLineIndex].LineText.ToCharArray())
		{
            if (i != 0 && i != (_currentConvo.MyLines[_currentLineIndex].LineText.Length - 1))
            {
                _dialogueText.text += _currentConvo.MyLines[_currentLineIndex].LineText[i];
            }

			yield return new WaitForSeconds(_dialogueManager.TypeSpeed);
		}
		_isWriting = false;
        if (!_lastLine)
            _currentLineIndex++;
    }
}

