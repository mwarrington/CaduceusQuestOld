using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGoal> EncounterGoals;
    public List<EncounterTurnType> EncounterTurns = new List<EncounterTurnType>();
    public Sprite DefalutButtonSprite, CheckedBox;
    public Sprite[] TrustProgressBarSprites;
    public GameObject PlayerMenu;
    public Encounter CurrentEncounter;

    private Transform _targetSpawnPoint;
    private GameManager _theGameManager;
    private Image _target1CB1, _target1CB2, _target1CB3,
        _target2CB1, _target2CB2, _target2CB3,
        _target3CB1, _target3CB2, _target3CB3,
        _target4CB1, _target4CB2, _target4CB3,
        _target1Trust, _target2Trust, _target3Trust, _target4Trust;
    private Text _targetName1, _targetName2, _targetName3, _targetName4,
        _treatment1, _treatment2, _treatment3, _treatment4,
        _encounterMessage, _speakerName;
    private Button _skills, _items, _endEncounter,
        _science, _engineering, _technology, _mathamatics, _communication,
        _sSkill1, _sSkill2, _sSkill3,
        _eSkill1, _eSkill2, _eSkill3,
        _tSkill1, _tSkill2, _tSkill3,
        _mSkill1, _mSkill2, _mSkill3,
        _cSkill1, _cSkill2, _cSkill3;

    private Button[] _baseMenu = new Button[3],
        _skillSubMenu = new Button[5];
    private List<Button> _sSkillSubMenu = new List<Button>(),
        _eSkillSubMenu = new List<Button>(),
        _tSkillSubMenu = new List<Button>(),
        _mSkillSubMenu = new List<Button>(),
        _cSkillSubMenu = new List<Button>();

    private List<string> _currentEncounterMessages = new List<string>();
    private GameObject _currentMinigameObj,
        _eaAnimation;
    private EncounterAction _currentEA;
    private EncounterMenus _activeMenu = EncounterMenus.BASEMENU;
    private Transform _skillAnimTransform;
    private int _turnIndex,
        _currentMessageIndex,
        _patientsTreated,
        _eventIndex;
    private bool _playerMenuEnabled,
        _encounterMessageEnabled,
        _encounterFinished,
        _skillAttemptFailed;

    protected EncounterActionDialog currentDialogEvent
    {
        get
        {
            return _currentDialogEvent;
        }

        set
        {
            if (_currentDialogEvent == value)
            {
                _allDialogEvents.Add(value);
                _currentDialogEvent = value;
            }
        }
    }

    private EncounterActionDialog _currentDialogEvent;
    private List<EncounterActionDialog> _allDialogEvents;

    #region Menu Index Properties

    //These handle image and text color change for menu navigation
    protected int baseMenuIndex
    {
        get { return _baseMenuIndex; }

        set
        {
            if (_baseMenuIndex != value)
            {
                if (_baseMenu[value].interactable)
                {
                    _baseMenu[_baseMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                    _baseMenu[_baseMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);

                    _baseMenu[value].GetComponent<Image>().sprite = _baseMenu[value].spriteState.highlightedSprite;
                    _baseMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);

                    _baseMenuIndex = value;
                }
                else
                {
                    int skipTo = SkipButton(_baseMenuIndex, value, _baseMenu.Length, _lastSkipIndexTry);
                    if (skipTo != -1)
                    {
                        _lastSkipIndexTry = skipTo;
                        baseMenuIndex = skipTo;
                    }

                    _lastSkipIndexTry = -1;
                }
            }
        }
    }

    protected int skillSubMenuIndex
    {
        get { return _skillSubMenuIndex; }

        set
        {
            if (_skillSubMenuIndex != value)
            {
                if (_skillSubMenu[value].interactable)
                {
                    if (_skillSubMenuIndex != -1)
                    {
                        _skillSubMenu[_skillSubMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                        _skillSubMenu[_skillSubMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                    }

                    if (value != -1)
                    {
                        _skillSubMenu[value].GetComponent<Image>().sprite = _skillSubMenu[value].spriteState.highlightedSprite;
                        _skillSubMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);
                    }

                    _skillSubMenuIndex = value;
                }
                else
                {
                    int skipTo = SkipButton(_skillSubMenuIndex, value, _skillSubMenu.Length, _lastSkipIndexTry);
                    if (skipTo != -1)
                    {
                        _lastSkipIndexTry = skipTo;
                        skillSubMenuIndex = skipTo;
                    }

                    _lastSkipIndexTry = -1;
                }
            }
        }
    }

    protected int sSkillSubMenuIndex
    {
        get { return _sSkillSubMenuIndex; }

        set
        {
            if (_sSkillSubMenuIndex != value)
            {
                if (_sSkillSubMenuIndex != -1)
                {
                    _sSkillSubMenu[_sSkillSubMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                    _sSkillSubMenu[_sSkillSubMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }

                if (value != -1)
                {
                    _sSkillSubMenu[value].GetComponent<Image>().sprite = _sSkillSubMenu[value].spriteState.highlightedSprite;
                    _sSkillSubMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);
                }

                _sSkillSubMenuIndex = value;
            }
        }
    }

    protected int eSkillSubMenuIndex
    {
        get { return _eSkillSubMenuIndex; }

        set
        {
            if (_eSkillSubMenuIndex != value)
            {
                if (_eSkillSubMenuIndex != -1)
                {
                    _eSkillSubMenu[_eSkillSubMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                    _eSkillSubMenu[_eSkillSubMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }

                if (value != -1)
                {
                    _eSkillSubMenu[value].GetComponent<Image>().sprite = _eSkillSubMenu[value].spriteState.highlightedSprite;
                    _eSkillSubMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);
                }

                _eSkillSubMenuIndex = value;
            }
        }
    }

    protected int tSkillSubMenuIndex
    {
        get { return _tSkillSubMenuIndex; }

        set
        {
            if (_tSkillSubMenuIndex != value)
            {
                if (_tSkillSubMenuIndex != -1)
                {
                    _tSkillSubMenu[_tSkillSubMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                    _tSkillSubMenu[_tSkillSubMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }

                if (value != -1)
                {
                    _tSkillSubMenu[value].GetComponent<Image>().sprite = _tSkillSubMenu[value].spriteState.highlightedSprite;
                    _tSkillSubMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);
                }

                _tSkillSubMenuIndex = value;
            }
        }
    }

    protected int mSkillSubMenuIndex
    {
        get { return _mSkillSubMenuIndex; }

        set
        {
            if (_mSkillSubMenuIndex != value)
            {
                if (_mSkillSubMenuIndex != -1)
                {
                    _mSkillSubMenu[_mSkillSubMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                    _mSkillSubMenu[_mSkillSubMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }

                if (value != -1)
                {
                    _mSkillSubMenu[value].GetComponent<Image>().sprite = _mSkillSubMenu[value].spriteState.highlightedSprite;
                    _mSkillSubMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);
                }

                _mSkillSubMenuIndex = value;
            }
        }
    }

    protected int cSkillSubMenuIndex
    {
        get { return _cSkillSubMenuIndex; }

        set
        {
            if (_cSkillSubMenuIndex != value)
            {
                if (_cSkillSubMenuIndex != -1)
                {
                    _cSkillSubMenu[_cSkillSubMenuIndex].GetComponent<Image>().sprite = DefalutButtonSprite;
                    _cSkillSubMenu[_cSkillSubMenuIndex].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }

                if (value != -1)
                {
                    _cSkillSubMenu[value].GetComponent<Image>().sprite = _cSkillSubMenu[value].spriteState.highlightedSprite;
                    _cSkillSubMenu[value].GetComponentInChildren<Text>().color = new Color(1, 1, 1);
                }

                _cSkillSubMenuIndex = value;
            }
        }
    }

    private int _baseMenuIndex = 0, _skillSubMenuIndex = -1, _sSkillSubMenuIndex = -1, _eSkillSubMenuIndex = -1, _tSkillSubMenuIndex = -1, _mSkillSubMenuIndex = -1, _cSkillSubMenuIndex = -1,
        _lastSkipIndexTry;

    #endregion Menu Index Properties

    #region Encounter Data Properties

    protected float target1CurrentTrust
    {
        get
        {
            return _target1CurrentTrust;
        }
        set
        {
            if (_target1CurrentTrust != value)
            {
                if (value <= 0)
                    _target1Trust.sprite = TrustProgressBarSprites[0];
                else if (value < 10)
                    _target1Trust.sprite = TrustProgressBarSprites[1];
                else if (value < 20)
                    _target1Trust.sprite = TrustProgressBarSprites[2];
                else if (value < 30)
                    _target1Trust.sprite = TrustProgressBarSprites[3];
                else if (value < 40)
                    _target1Trust.sprite = TrustProgressBarSprites[4];
                else if (value < 50)
                    _target1Trust.sprite = TrustProgressBarSprites[5];
                else if (value < 60)
                    _target1Trust.sprite = TrustProgressBarSprites[6];
                else if (value < 70)
                    _target1Trust.sprite = TrustProgressBarSprites[7];
                else if (value < 80)
                    _target1Trust.sprite = TrustProgressBarSprites[8];
                else if (value < 90)
                    _target1Trust.sprite = TrustProgressBarSprites[9];
                else if (value >= 90)
                    _target1Trust.sprite = TrustProgressBarSprites[10];

                _target1CurrentTrust = value;
            }
        }

    }

    protected float target2CurrentTrust
    {
        get
        {
            return _target2CurrentTrust;
        }
        set
        {
            if (_target2CurrentTrust != value)
            {
                if (value == 0)
                    _target2Trust.sprite = TrustProgressBarSprites[0];
                else if (value < 10)
                    _target2Trust.sprite = TrustProgressBarSprites[1];
                else if (value < 20)
                    _target2Trust.sprite = TrustProgressBarSprites[2];
                else if (value < 30)
                    _target2Trust.sprite = TrustProgressBarSprites[3];
                else if (value < 40)
                    _target2Trust.sprite = TrustProgressBarSprites[4];
                else if (value < 50)
                    _target2Trust.sprite = TrustProgressBarSprites[5];
                else if (value < 60)
                    _target2Trust.sprite = TrustProgressBarSprites[6];
                else if (value < 70)
                    _target2Trust.sprite = TrustProgressBarSprites[7];
                else if (value < 80)
                    _target2Trust.sprite = TrustProgressBarSprites[8];
                else if (value < 90)
                    _target2Trust.sprite = TrustProgressBarSprites[9];
                else if (value >= 90)
                    _target2Trust.sprite = TrustProgressBarSprites[10];

                _target2CurrentTrust = value;
            }
        }
    }

    protected float target3CurrentTrust
    {
        get
        {
            return _target3CurrentTrust;
        }
        set
        {
            if (_target3CurrentTrust != value)
            {
                if (value == 0)
                    _target3Trust.sprite = TrustProgressBarSprites[0];
                else if (value < 10)
                    _target3Trust.sprite = TrustProgressBarSprites[1];
                else if (value < 20)
                    _target3Trust.sprite = TrustProgressBarSprites[2];
                else if (value < 30)
                    _target3Trust.sprite = TrustProgressBarSprites[3];
                else if (value < 40)
                    _target3Trust.sprite = TrustProgressBarSprites[4];
                else if (value < 50)
                    _target3Trust.sprite = TrustProgressBarSprites[5];
                else if (value < 60)
                    _target3Trust.sprite = TrustProgressBarSprites[6];
                else if (value < 70)
                    _target3Trust.sprite = TrustProgressBarSprites[7];
                else if (value < 80)
                    _target3Trust.sprite = TrustProgressBarSprites[8];
                else if (value < 90)
                    _target3Trust.sprite = TrustProgressBarSprites[9];
                else if (value >= 90)
                    _target3Trust.sprite = TrustProgressBarSprites[10];

                _target3CurrentTrust = value;
            }
        }
    }

    protected float target4CurrentTrust
    {
        get
        {
            return _target4CurrentTrust;
        }
        set
        {
            if (_target4CurrentTrust != value)
            {
                if (value == 0)
                    _target4Trust.sprite = TrustProgressBarSprites[0];
                else if (value < 10)
                    _target4Trust.sprite = TrustProgressBarSprites[1];
                else if (value < 20)
                    _target4Trust.sprite = TrustProgressBarSprites[2];
                else if (value < 30)
                    _target4Trust.sprite = TrustProgressBarSprites[3];
                else if (value < 40)
                    _target4Trust.sprite = TrustProgressBarSprites[4];
                else if (value < 50)
                    _target4Trust.sprite = TrustProgressBarSprites[5];
                else if (value < 60)
                    _target4Trust.sprite = TrustProgressBarSprites[6];
                else if (value < 70)
                    _target4Trust.sprite = TrustProgressBarSprites[7];
                else if (value < 80)
                    _target4Trust.sprite = TrustProgressBarSprites[8];
                else if (value < 90)
                    _target4Trust.sprite = TrustProgressBarSprites[9];
                else if (value >= 90)
                    _target4Trust.sprite = TrustProgressBarSprites[10];

                _target4CurrentTrust = value;
            }
        }
    }

    private float _target1CurrentTrust, _target2CurrentTrust, _target3CurrentTrust, _target4CurrentTrust;

    private int _target1SuccessCount, _target2SuccessCount, _target3SuccessCount, _target4SuccessCount;

    #endregion Encounter Data Properties

    private int _sSkillCount, _eSkillCount, _tSkillCount, _mSkillCount, _cSkillCount;

    private void Start()
    {
        _theGameManager = FindObjectOfType<GameManager>();

        #region Getting/Setting Encounter Buttons
        _skills = GameObject.Find("Skills").GetComponent<Button>();
        _items = GameObject.Find("Items").GetComponent<Button>();
        _endEncounter = GameObject.Find("End Encounter").GetComponent<Button>();
        _science = GameObject.Find("Science").GetComponent<Button>();
        _engineering = GameObject.Find("Engineering").GetComponent<Button>();
        _technology = GameObject.Find("Technology").GetComponent<Button>();
        _mathamatics = GameObject.Find("Mathamatics").GetComponent<Button>();
        _communication = GameObject.Find("Communication").GetComponent<Button>();
        _sSkill1 = GameObject.Find("Science Skill 1").GetComponent<Button>();
        _sSkill2 = GameObject.Find("Science Skill 2").GetComponent<Button>();
        _sSkill3 = GameObject.Find("Science Skill 3").GetComponent<Button>();
        _eSkill1 = GameObject.Find("Engineering Skill 1").GetComponent<Button>();
        _eSkill2 = GameObject.Find("Engineering Skill 2").GetComponent<Button>();
        _eSkill3 = GameObject.Find("Engineering Skill 3").GetComponent<Button>();
        _tSkill1 = GameObject.Find("Technology Skill 1").GetComponent<Button>();
        _tSkill2 = GameObject.Find("Technology Skill 2").GetComponent<Button>();
        _tSkill3 = GameObject.Find("Technology Skill 3").GetComponent<Button>();
        _mSkill1 = GameObject.Find("Mathamatics Skill 1").GetComponent<Button>();
        _mSkill2 = GameObject.Find("Mathamatics Skill 2").GetComponent<Button>();
        _mSkill3 = GameObject.Find("Mathamatics Skill 3").GetComponent<Button>();
        _cSkill1 = GameObject.Find("Communication Skill 1").GetComponent<Button>();
        _cSkill2 = GameObject.Find("Communication Skill 2").GetComponent<Button>();
        _cSkill3 = GameObject.Find("Communication Skill 3").GetComponent<Button>();

        _science.transform.parent.gameObject.SetActive(false);
        _sSkill1.transform.parent.gameObject.SetActive(false);
        _eSkill1.transform.parent.gameObject.SetActive(false);
        _tSkill1.transform.parent.gameObject.SetActive(false);
        _mSkill1.transform.parent.gameObject.SetActive(false);
        _cSkill1.transform.parent.gameObject.SetActive(false);

        _baseMenu[0] = _skills;
        _baseMenu[1] = _items;
        _baseMenu[2] = _endEncounter;

        _skillSubMenu[0] = _science;
        _skillSubMenu[1] = _engineering;
        _skillSubMenu[2] = _technology;
        _skillSubMenu[3] = _mathamatics;
        _skillSubMenu[4] = _communication;

        PopulateTypeSkillSubMenu();
        #endregion Getting/Setting Encounter Buttons

        #region Getting/Setting Encounter Data
        _targetName1 = GameObject.Find("Target 1/Target Name").GetComponent<Text>();
        _targetName2 = GameObject.Find("Target 2/Target Name").GetComponent<Text>();
        _targetName3 = GameObject.Find("Target 3/Target Name").GetComponent<Text>();
        _targetName4 = GameObject.Find("Target 4/Target Name").GetComponent<Text>();
        _treatment1 = GameObject.Find("Target 1/Treatment/Treatment Label").GetComponent<Text>();
        _treatment2 = GameObject.Find("Target 2/Treatment/Treatment Label").GetComponent<Text>();
        _treatment3 = GameObject.Find("Target 3/Treatment/Treatment Label").GetComponent<Text>();
        _treatment4 = GameObject.Find("Target 4/Treatment/Treatment Label").GetComponent<Text>();
        _target1CB1 = GameObject.Find("Target 1/Treatment/Treatment Checkbox 1").GetComponent<Image>();
        _target1CB2 = GameObject.Find("Target 1/Treatment/Treatment Checkbox 2").GetComponent<Image>();
        _target1CB3 = GameObject.Find("Target 1/Treatment/Treatment Checkbox 3").GetComponent<Image>();
        _target2CB1 = GameObject.Find("Target 2/Treatment/Treatment Checkbox 1").GetComponent<Image>();
        _target2CB2 = GameObject.Find("Target 2/Treatment/Treatment Checkbox 2").GetComponent<Image>();
        _target2CB3 = GameObject.Find("Target 2/Treatment/Treatment Checkbox 3").GetComponent<Image>();
        _target3CB1 = GameObject.Find("Target 3/Treatment/Treatment Checkbox 1").GetComponent<Image>();
        _target3CB2 = GameObject.Find("Target 3/Treatment/Treatment Checkbox 2").GetComponent<Image>();
        _target3CB3 = GameObject.Find("Target 3/Treatment/Treatment Checkbox 3").GetComponent<Image>();
        _target4CB1 = GameObject.Find("Target 4/Treatment/Treatment Checkbox 1").GetComponent<Image>();
        _target4CB2 = GameObject.Find("Target 4/Treatment/Treatment Checkbox 2").GetComponent<Image>();
        _target4CB3 = GameObject.Find("Target 4/Treatment/Treatment Checkbox 3").GetComponent<Image>();
        _target1Trust = GameObject.Find("Target 1/Trust Level/Image").GetComponent<Image>();
        _target2Trust = GameObject.Find("Target 2/Trust Level/Image").GetComponent<Image>();
        _target3Trust = GameObject.Find("Target 3/Trust Level/Image").GetComponent<Image>();
        _target4Trust = GameObject.Find("Target 4/Trust Level/Image").GetComponent<Image>();

        _speakerName = GameObject.Find("EncounterMessage/Speaker Name").GetComponent<Text>();


        _encounterMessage = GameObject.Find("EncounterMessage").GetComponentInChildren<Text>();
        _encounterMessage.transform.parent.gameObject.SetActive(false);
        EncounterInfoInitializer();
        #endregion Getting/Setting Encounter Data

        CreateTurnOrder();
        PlayerMenu.SetActive(false);

        BeginTurn();
    }

    private void BeginTurn()
    {
        if (_turnIndex == EncounterTurns.Count)
        {
            switch (CurrentEncounter.TurnPattern)
            {
                case EncounterPattern.ALTERNATE:
                    if (EncounterTurns[_turnIndex - 1] == EncounterTurnType.EVENT)
                    {
                        EncounterTurns.Add(EncounterTurnType.PLAYER);
                    }
                    else if (EncounterTurns[_turnIndex - 1] == EncounterTurnType.PLAYER)
                    {
                        EncounterTurns.Add(EncounterTurnType.EVENT);
                    }
                    break;
                case EncounterPattern.DOUBLEALTERNATE:
                    break;
                case EncounterPattern.PLAYER1DIALOG2:
                    break;
                case EncounterPattern.PLAYER2DIALOG1:
                    break;
            }
        }

        if (EncounterTurns[_turnIndex] == EncounterTurnType.PLAYER)
        {
            TogglePlayerMenu();
        }
        else if (EncounterTurns[_turnIndex] == EncounterTurnType.EVENT)
        {
            LoadDialogEvent();

            DisplayEncounterMessage(_currentDialogEvent.Speaker, _currentDialogEvent.SpeakerLine, _currentDialogEvent.LineEmotion);
            PrepMiniGameToInstantiate(_currentDialogEvent.Name);
        }

        _turnIndex++;
    }

    private void LoadDialogEvent()
    {
        int rand = Random.Range(0, CurrentEncounter.GoalCount);
        _eventIndex++;
        string path = "EncounterActions/" + CurrentEncounter.EncounterGoals[rand].Subject + "/" + CurrentEncounter.EncounterGoals[rand].ActionName + _eventIndex;

        _currentDialogEvent = Resources.Load<EncounterActionDialog>(path);

        if (_currentDialogEvent == null)
        {
            _eventIndex = 1;
            path = "EncounterActions/" + CurrentEncounter.EncounterGoals[rand].Subject + "/" + CurrentEncounter.EncounterGoals[rand].ActionName + _eventIndex;
            _currentDialogEvent = Resources.Load<EncounterActionDialog>(path);
        }

    }

    private void CreateTurnOrder()
    {
        bool makingPlayer = true;

        switch (CurrentEncounter.TurnPattern)
        {
            case EncounterPattern.ALTERNATE:
                for (int i = 0; i < 3; i++)
                {
                    if (makingPlayer)
                    {
                        EncounterTurns.Add(EncounterTurnType.PLAYER);
                    }
                    else
                    {
                        EncounterTurns.Add(EncounterTurnType.EVENT);
                    }

                    makingPlayer = !makingPlayer;
                }
                break;
            case EncounterPattern.DOUBLEALTERNATE:
                for (int i = 0; i < 4; i++)
                {
                    if (i == 2)
                        makingPlayer = false;

                    if (makingPlayer)
                    {
                        EncounterTurns.Add(EncounterTurnType.PLAYER);
                    }
                    else
                    {
                        EncounterTurns.Add(EncounterTurnType.EVENT);
                    }
                }
                break;
            case EncounterPattern.PLAYER1DIALOG2:
                for (int i = 0; i < 3; i++)
                {
                    if (i == 1)
                        makingPlayer = false;

                    if (makingPlayer)
                    {
                        EncounterTurns.Add(EncounterTurnType.PLAYER);
                    }
                    else
                    {
                        EncounterTurns.Add(EncounterTurnType.EVENT);
                    }
                }
                break;
            case EncounterPattern.PLAYER2DIALOG1:
                for (int i = 0; i < 3; i++)
                {
                    if (i == 2)
                        makingPlayer = false;

                    if (makingPlayer)
                    {
                        EncounterTurns.Add(EncounterTurnType.PLAYER);
                    }
                    else
                    {
                        EncounterTurns.Add(EncounterTurnType.EVENT);
                    }
                }
                break;
        }
    }

    private void Update()
    {
        if (_encounterMessageEnabled)
            MessageInputHandler();

        if (_playerMenuEnabled)
            UIInputHandler();
    }

    private void MessageInputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (_currentMessageIndex == _currentEncounterMessages.Count)
            {
                HideEncounterMessage();

                if (_encounterFinished)
                {
                    EndEncounter();
                }
                else if (_currentMinigameObj != null)
                {
                    LoadMinigame();
                }
                else
                    BeginTurn();
            }
            else
            {
                DisplayEncounterMessage();
            }
        }
    }

    public void LoadVoluntaryEnd()
    {
        _encounterFinished = true;
        TogglePlayerMenu();
        DisplayEncounterMessage("SIMONE", "Looks like we'll have to come back to this one.");
    }

    public void EndEncounter()
    {
        string sceneToLoad = "",
        currentSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < currentSceneName.Length; i++)
        {
            if (currentSceneName[i] != 'E')
            {
                sceneToLoad += currentSceneName[i];
            }
            else
                break;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    private void UIInputHandler()
    {
        List<Button> currentMenu = new List<Button>();
        int currentIndex = -2;

        switch (_activeMenu)
        {
            case EncounterMenus.BASEMENU:
                currentMenu.AddRange(_baseMenu);
                currentIndex = baseMenuIndex;
                break;
            case EncounterMenus.SKILLSUBMENU:
                currentMenu.AddRange(_skillSubMenu);
                currentIndex = skillSubMenuIndex;
                break;
            case EncounterMenus.SSKILLSUBMENU:
                currentMenu.AddRange(_sSkillSubMenu);
                currentIndex = sSkillSubMenuIndex;
                break;
            case EncounterMenus.ESKILLSUBMENU:
                currentMenu.AddRange(_eSkillSubMenu);
                currentIndex = eSkillSubMenuIndex;
                break;
            case EncounterMenus.TSKILLSUBMENU:
                currentMenu.AddRange(_tSkillSubMenu);
                currentIndex = tSkillSubMenuIndex;
                break;
            case EncounterMenus.MSKILLSUBMENU:
                currentMenu.AddRange(_mSkillSubMenu);
                currentIndex = mSkillSubMenuIndex;
                break;
            case EncounterMenus.CSKILLSUBMENU:
                currentMenu.AddRange(_cSkillSubMenu);
                currentIndex = cSkillSubMenuIndex;
                break;
            default:
                break;
        }

        #region Verticle Menu Naviagion Controls
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;

            if (currentIndex == currentMenu.Count)
            {
                currentIndex = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;

            if (currentIndex < 0)
            {
                currentIndex = currentMenu.Count - 1;
            }
        }

        switch (_activeMenu)
        {
            case EncounterMenus.BASEMENU:
                baseMenuIndex = currentIndex;
                break;
            case EncounterMenus.SKILLSUBMENU:
                skillSubMenuIndex = currentIndex;
                break;
            case EncounterMenus.SSKILLSUBMENU:
                sSkillSubMenuIndex = currentIndex;
                break;
            case EncounterMenus.ESKILLSUBMENU:
                eSkillSubMenuIndex = currentIndex;
                break;
            case EncounterMenus.TSKILLSUBMENU:
                tSkillSubMenuIndex = currentIndex;
                break;
            case EncounterMenus.MSKILLSUBMENU:
                mSkillSubMenuIndex = currentIndex;
                break;
            case EncounterMenus.CSKILLSUBMENU:
                cSkillSubMenuIndex = currentIndex;
                break;
            default:
                break;
        }
        #endregion Verticle Menu Naviagion Controls

        #region Horizontal Menu Naviagion and Select Controls
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightArrow)) && currentMenu[currentIndex].interactable)
        {
            bool correctMinigame = false;
            for (int i = 0; i < EncounterGoals.Count; i++)
            {
                if (currentMenu[currentIndex].GetComponentInChildren<Text>().text == EncounterGoals[i].ActionName)
                {
                    correctMinigame = true;
                    break;
                }
            }

            if (!correctMinigame && currentMenu[currentIndex].tag == "Skill Button")
            {
                TogglePlayerMenu();
                DisplayEncounterMessage(CurrentEncounter.EncounterGoals[0].Subject, "I don't want you to do that!");
            }
            else
                currentMenu[currentIndex].onClick.Invoke();

            switch (currentMenu[currentIndex].name)
            {
                case "Skills":
                    _activeMenu = EncounterMenus.SKILLSUBMENU;
                    skillSubMenuIndex = 0;
                    break;
                case "Science":
                    _activeMenu = EncounterMenus.SSKILLSUBMENU;
                    sSkillSubMenuIndex = 0;
                    break;
                case "Engineering":
                    _activeMenu = EncounterMenus.ESKILLSUBMENU;
                    eSkillSubMenuIndex = 0;
                    break;
                case "Technology":
                    _activeMenu = EncounterMenus.TSKILLSUBMENU;
                    tSkillSubMenuIndex = 0;
                    break;
                case "Mathamatics":
                    _activeMenu = EncounterMenus.MSKILLSUBMENU;
                    mSkillSubMenuIndex = 0;
                    break;
                case "Communication":
                    _activeMenu = EncounterMenus.CSKILLSUBMENU;
                    cSkillSubMenuIndex = 0;
                    break;
            }
        }

        if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftArrow)) && _activeMenu != EncounterMenus.BASEMENU)
        {
            _activeMenu = EncounterMenus.BASEMENU;
            string buttonName = currentMenu[currentIndex].name;

            if (buttonName != "Science" && buttonName != "Engineering" && buttonName != "Technology" && buttonName != "Mathamatics" && buttonName != "Communication")
            {
                _activeMenu = EncounterMenus.SKILLSUBMENU;

                switch (currentMenu[currentIndex].transform.parent.parent.name)
                {
                    case "Science":
                        sSkillSubMenuIndex = -1;
                        break;
                    case "Engineering":
                        eSkillSubMenuIndex = -1;
                        break;
                    case "Technology":
                        tSkillSubMenuIndex = -1;
                        break;
                    case "Mathamatics":
                        mSkillSubMenuIndex = -1;
                        break;
                    case "Communication":
                        cSkillSubMenuIndex = -1;
                        break;
                    default:
                        Debug.LogWarning(currentMenu[currentIndex].transform.parent.parent.name + " shouldn't exist");
                        break;
                }
            }
            
            HideSubMenu(currentMenu[currentIndex].transform.parent.gameObject);
        }
        #endregion Horizontal Menu Naviagion and Select Controls
    }

    public void PrepMiniGameToInstantiate(Text myText)
    {
        string skillName = myText.text;
        
        _currentEA = Resources.Load<EncounterAction>("EncounterActions/" + skillName);

        switch (_currentEA.MyType)
        {
            case EncounterActionType.COMPSCI:
                EncounterActionCompSci myCompSciSO = (EncounterActionCompSci)_currentEA;
                _currentMinigameObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/CompSciPuzzle/CompSciPuzzle" + myCompSciSO.SymbolCount);
                _currentEncounterMessages.Add(myCompSciSO.Name);
                DisplayEncounterMessage(myCompSciSO.Name);
                TogglePlayerMenu();
                break;
            case EncounterActionType.DOCTOR:
                EncounterActionDoctor myDoctorSO = (EncounterActionDoctor)_currentEA;
                _currentMinigameObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/Doctor/DoctorPuzzleDefault");
                DisplayEncounterMessage(myDoctorSO.Name);
                TogglePlayerMenu();
                break;
            case EncounterActionType.DIALOG:
                EncounterActionDialog myDialogSO = (EncounterActionDialog)_currentEA;
                _currentMinigameObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/Dialog/DialogPuzzle");
                //DisplayEncounterMessage(_currentEncounterMessages[0], false);
                break;
            default:
                Debug.LogError("We haven't put together an IntiateAction for that action type.");
                break;
        }
    }

    public void PrepMiniGameToInstantiate(string myText)
    {
        string skillName = myText;

        if (_currentDialogEvent == null)
            _currentEA = Resources.Load<EncounterAction>("EncounterActions/" + skillName);
        else
            _currentEA = _currentDialogEvent;

        switch (_currentEA.MyType)
        {
            case EncounterActionType.COMPSCI:
                EncounterActionCompSci myCompSciSO = (EncounterActionCompSci)_currentEA;
                _currentMinigameObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/CompSciPuzzle/CompSciPuzzle" + myCompSciSO.SymbolCount);
                _currentEncounterMessages.Add(myCompSciSO.Name);
                DisplayEncounterMessage(myCompSciSO.Name);
                TogglePlayerMenu();
                break;
            case EncounterActionType.DOCTOR:
                EncounterActionDoctor myDoctorSO = (EncounterActionDoctor)_currentEA;
                _currentMinigameObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/Doctor/DoctorPuzzleDefault");
                DisplayEncounterMessage(myDoctorSO.Name);
                TogglePlayerMenu();
                break;
            case EncounterActionType.DIALOG:
                EncounterActionDialog myDialogSO = (EncounterActionDialog)_currentEA;
                _currentMinigameObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/Dialog/DialogPuzzle");
                //DisplayEncounterMessage(_currentEncounterMessages[0], false);
                break;
            default:
                Debug.LogError("We haven't put together an IntiateAction for that action type.");
                break;
        }
    }

    private void LoadMinigame()
    {
        switch (_currentEA.MyType)
        {
            case EncounterActionType.COMPSCI:
                EncounterActionCompSci myCompSciSO = (EncounterActionCompSci)_currentEA;
                _currentMinigameObj = GameObject.Instantiate(_currentMinigameObj);
                CompSciPuzzleManager cspm = _currentMinigameObj.GetComponent<CompSciPuzzleManager>();
                cspm.Strikes = myCompSciSO.StrikeCount;
                cspm.Name = myCompSciSO.Name;
                cspm.FailPenalty = myCompSciSO.FailPenalty;
                break;
            case EncounterActionType.DOCTOR:
                EncounterActionDoctor myDoctorSO = (EncounterActionDoctor)_currentEA;
                _currentMinigameObj = GameObject.Instantiate(_currentMinigameObj, new Vector3(this.transform.position.x - 1000f, this.transform.position.y - 1000f, this.transform.position.z), Quaternion.identity);
                DoctorPuzzleManager myDocPuzzMan = _currentMinigameObj.GetComponent<DoctorPuzzleManager>();
                myDocPuzzMan.Name = myDoctorSO.Name;
                myDocPuzzMan.FailPenalty = myDoctorSO.FailPenalty;
                myDocPuzzMan.KeyStrokeCount = myDoctorSO.KeyStrokeCount;
                myDocPuzzMan.MinArrowSpeed = myDoctorSO.ArrowMinSpeed;
                myDocPuzzMan.MaxArrowSpeed = myDoctorSO.ArrowMaxSpeed;
                myDocPuzzMan.SpawnRate = myDoctorSO.SpawnRate;
                break;
            case EncounterActionType.DIALOG:
                EncounterActionDialog myDialogSO = (EncounterActionDialog)_currentEA;
                _currentMinigameObj = GameObject.Instantiate(_currentMinigameObj, new Vector3(this.transform.position.x - 1000f, this.transform.position.y - 1000f, this.transform.position.z), Quaternion.identity);
                DialogPuzzleManager myDialogPuzzMan = _currentMinigameObj.GetComponent<DialogPuzzleManager>();
                myDialogPuzzMan.Name = myDialogSO.Name;
                myDialogPuzzMan.FailPenalty = myDialogSO.FailPenalty;
                myDialogPuzzMan.Speaker = myDialogSO.Speaker;
                myDialogPuzzMan.SpeakerLine = myDialogSO.SpeakerLine;
                myDialogPuzzMan.BadResponse = myDialogSO.BadResponse;
                myDialogPuzzMan.GoodResponse = myDialogSO.GoodResponse;
                myDialogPuzzMan.LineEmotion = myDialogSO.LineEmotion;
                break;
            default:
                Debug.LogError("We haven't put together an IntiateAction for that action type.");
                break;
        }

        if (_currentEA.MyType != EncounterActionType.DIALOG)
        {
            _skillAnimTransform = GameObject.Find("SkillAnimLoadPoint").transform;
            string path = "Prefabs/" + _currentEA.Name + "Animation";
            path = path.Replace(" ", "");
            _eaAnimation = Resources.Load<GameObject>(path);
            _eaAnimation = GameObject.Instantiate(_eaAnimation, _skillAnimTransform.position, _skillAnimTransform.rotation);
        }

        _currentMinigameObj = null;
    }

    #region Puzzle Win Fail Methods

    public void PuzzleFail(float failPenalty, GameObject currentPuzzle, EncounterActionType myType)
    {
        for (int i = 0; i < EncounterGoals.Count; i++)
        {
            if (myType == EncounterActionType.DIALOG)
            {
                if (EncounterGoals[i].Subject == _currentDialogEvent.Speaker)
                {
                    if (i == 0)
                    {
                        target1CurrentTrust -= failPenalty;
                    }
                    if (i == 1)
                    {
                        target2CurrentTrust -= failPenalty;
                    }
                    if (i == 2)
                    {
                        target3CurrentTrust -= failPenalty;
                    }
                    if (i == 3)
                    {
                        target4CurrentTrust -= failPenalty;
                    }
                }
            }
            else if (EncounterGoals[i].ActionType == myType)
            {
                if (i == 0)
                {
                    target1CurrentTrust -= failPenalty;
                }
                if (i == 1)
                {
                    target2CurrentTrust -= failPenalty;
                }
                if (i == 2)
                {
                    target3CurrentTrust -= failPenalty;
                }
                if (i == 3)
                {
                    target4CurrentTrust -= failPenalty;
                }
            }
        }

        if (myType == EncounterActionType.DIALOG)
        {
            DisplayEncounterMessage(currentDialogEvent.BadResponse);
        }

        if (target1CurrentTrust > 0 || target2CurrentTrust > 0 || target3CurrentTrust > 0 || target4CurrentTrust > 0)
        {

            DisplayEncounterMessage("SIMONE", "Dang, that didn't go so well...");
        }
        else
        {
            _encounterFinished = true;
            string[] failMessages = new string[2];
            failMessages[0] = "Oh boy... They don't seem happy.";
            failMessages[1] = "Maybe we should try again later...";
            DisplayEncounterMessage("Simone", failMessages);
            _theGameManager.DialogueChanger(CurrentEncounter.EncounterGoals[0].Subject, DialogChangeType.ENCOUNTERFAIL);
        }
        GameObject.Destroy(currentPuzzle);
        GameObject.Destroy(_eaAnimation);
    }

    //Event Puzzle Win
    public void PuzzleWin(float failPenalty, GameObject currentPuzzle)
    {
        for (int i = 0; i < EncounterGoals.Count; i++)
        {
            if (EncounterGoals[i].Subject == _currentDialogEvent.Speaker)
            {
                if (i == 0)
                {
                    target1CurrentTrust += failPenalty;
                }
                if (i == 1)
                {
                    target2CurrentTrust += failPenalty;
                }
                if (i == 2)
                {
                    target3CurrentTrust += failPenalty;
                }
                if (i == 3)
                {
                    target4CurrentTrust += failPenalty;
                }
            }
        }

        string[] dialogs = new string[2];
        dialogs[0] = _currentDialogEvent.GoodResponse;
        dialogs[1] = "Good Job!";
        DisplayEncounterMessage("Simone", dialogs);
        //MASON: Continue here
        GameObject.Destroy(currentPuzzle);
        GameObject.Destroy(_eaAnimation);
    }

    //Player puzzle win
    public void PuzzleWin(string actionName, GameObject currentPuzzle)
    {
        bool loadWinMessage = false;
        int lastTreatedPatientCount = _patientsTreated;

        for (int i = 0; i < EncounterGoals.Count; i++)
        {
            if (EncounterGoals[i].ActionName == actionName)
            {
                if (i == 0)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target1SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target1CB1.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target1CB2.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target1CB3.sprite = CheckedBox;
                        loadWinMessage = true;
                    }

                    _target1SuccessCount++;

                    if (_target1SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        loadWinMessage = false;
                        _patientsTreated++;
                        _theGameManager.CurrentEvent.GoalReached(EncounterGoals[i].Subject, EncounterGoals[i].ActionName);
                        //True win
                    }

                    GameObject.Destroy(currentPuzzle);
                    GameObject.Destroy(_eaAnimation);
                }
                if (i == 1)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target2SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target2CB1.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target2CB2.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target2CB3.sprite = CheckedBox;
                        loadWinMessage = true;
                    }

                    _target2SuccessCount++;

                    if (_target2SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        loadWinMessage = false;
                        _patientsTreated++;
                        //True win
                    }

                    GameObject.Destroy(currentPuzzle);
                    GameObject.Destroy(_eaAnimation);
                }
                if (i == 2)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target3SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target3CB1.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target3CB2.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target3CB3.sprite = CheckedBox;
                        loadWinMessage = true;
                    }

                    _target3SuccessCount++;

                    if (_target3SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        loadWinMessage = false;
                        _patientsTreated++;
                        //True win
                    }

                    GameObject.Destroy(currentPuzzle);
                    GameObject.Destroy(_eaAnimation);
                }
                if (i == 3)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target4SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target4CB1.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target4CB2.sprite = CheckedBox;
                        loadWinMessage = true;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target4CB3.sprite = CheckedBox;
                        loadWinMessage = true;
                    }

                    _target4SuccessCount++;

                    if (_target3SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        loadWinMessage = false;
                        _patientsTreated++;
                        //True win
                    }

                    GameObject.Destroy(currentPuzzle);
                    GameObject.Destroy(_eaAnimation);
                }
            }
        }

        if (loadWinMessage)
        {
            DisplayEncounterMessage("Good Job!");
        }
        else if (_patientsTreated > lastTreatedPatientCount)
        {
            if (_patientsTreated == CurrentEncounter.GoalCount)
            {
                _encounterFinished = true;
                string[] winMessages = new string[2];
                winMessages[0] = "Wonderful!";
                winMessages[1] = "You've treated all patients";
                DisplayEncounterMessage(winMessages);
                _theGameManager.DialogueChanger(CurrentEncounter.EncounterGoals[0].Subject, DialogChangeType.ENCOUNTERWIN);
            }
            else
                DisplayEncounterMessage("Great Job! Patient treated!");
        }
    }

    #endregion Puzzle Win Fail Methods

    private void TogglePlayerMenu()
    {
        if (PlayerMenu.activeSelf)
            ResetMenu();

        PlayerMenu.SetActive(!PlayerMenu.activeSelf);
        _playerMenuEnabled = !_playerMenuEnabled;
    }

    #region Display Encounter Message Methods

    //For displaying next in a set
    private void DisplayEncounterMessage()
    {
        _encounterMessage.transform.parent.gameObject.SetActive(true);
        _encounterMessage.text = _currentEncounterMessages[_currentMessageIndex];
        _encounterMessageEnabled = true;
        Image messageBox = _encounterMessage.transform.parent.GetComponent<Image>();
        messageBox.color = new Color(1f, 1f, 1f, 0.5f);
        Text emotionCoordinate = GameObject.Find("EncounterMessage/EmotionCoordinate").GetComponent<Text>();
        emotionCoordinate.text = "";
        _currentMessageIndex++;
    }

    //For displaying or adding one message
    private void DisplayEncounterMessage(string message)
    {
        if (!_encounterMessageEnabled)
        {
            _currentEncounterMessages.Clear();
            _currentMessageIndex = 0;
            _encounterMessage.transform.parent.gameObject.SetActive(true);
            _currentEncounterMessages.Add(message);
            _encounterMessage.text = _currentEncounterMessages[_currentMessageIndex];
            _encounterMessageEnabled = true;
            _currentMessageIndex++;
        }
        else
        {
            _currentEncounterMessages.Add(message);
        }
        
        Image messageBox = _encounterMessage.transform.parent.GetComponent<Image>();
        messageBox.color = new Color(1f, 1f, 1f, 0.5f);
        Text emotionCoordinate = GameObject.Find("EncounterMessage/EmotionCoordinate").GetComponent<Text>();
        emotionCoordinate.text = "";
    }

    private void DisplayEncounterMessage(string SpeakerName, string message)
    {
        _speakerName.text = SpeakerName.ToUpper() + ":";
        DisplayEncounterMessage(message);
    }

    private void DisplayEncounterMessage(string SpeakerName, string message, Emotion messageEmotion)
    {
       
        _speakerName.text = SpeakerName.ToUpper() + ":";
        _currentEncounterMessages.Clear();
        _currentEncounterMessages.Add(message);
        _currentMessageIndex = 0;

        _encounterMessage.transform.parent.gameObject.SetActive(true);
        _encounterMessage.text = _currentEncounterMessages[_currentMessageIndex];
        Image messageBox = _encounterMessage.transform.parent.GetComponent<Image>();
        Text emotionCoordinate = GameObject.Find("EncounterMessage/EmotionCoordinate").GetComponent<Text>();
        emotionCoordinate.text = messageEmotion.EmotionType + ", " + messageEmotion.EmotionIntensity;
        _encounterMessageEnabled = true;

        switch (messageEmotion.EmotionType)
        {
            case 'a':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.99f, .99f, .8f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.99f, .95f, .51f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(0.97f, 0.85f, 0.30f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(.93f, .76f, 0, 1);
                        break;
                }
                break;
            case 'b':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.85f, .92f, .62f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.77f, .88f, .38f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.6f, .8f, .2f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(.48f, .74f, .05f, 1);
                        break;
                }
                break;
            case 'c':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.8f, .93f, .80f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.46f, .76f, .47f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.21f, .64f, .31f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(0, .45f, .18f, 1);
                        break;
                }
                break;
            case 'd':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.85f, .92f, .62f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.05f, .78f, .84f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.24f, .64f, .75f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(0, .51f, .67f, 1);
                        break;
                }
                break;
            case 'e':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.79f, .87f, .92f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.64f, .73f, .85f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.45f, .62f, .79f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(.12f, .42f, .67f, 1);
                        break;
                }
                break;
            case 'f':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.80f, .70f, .85f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.73f, .6f, .8f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.62f, .47f, .73f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(.48f, .31f, .64f, 1);
                        break;
                }
                break;
            case 'g':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.93f, .65f, .64f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.77f, .88f, .38f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.90f, .19f, .36f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(.86f, 0f, .28f, 1);
                        break;
                }
                break;
            case 'h':
                switch (messageEmotion.EmotionIntensity)
                {
                    case 1:
                        messageBox.color = new Color(.99f, .85f, .64f, 1);
                        break;
                    case 2:
                        messageBox.color = new Color(.97f, .73f, .41f, 1);
                        break;
                    case 3:
                        messageBox.color = new Color(.95f, .6f, .23f, 1);
                        break;
                    case 4:
                        messageBox.color = new Color(.91f, .44f, .0f, 1);
                        break;
                }
                break;
        }
        _currentMessageIndex++;
    }

    //For begining and displaying a new set
    private void DisplayEncounterMessage(string SpeakerName, string[] messages)
    {
        _speakerName.text = SpeakerName.ToUpper() + ":";
        DisplayEncounterMessage(messages);
       
    }

    private void DisplayEncounterMessage(string[] messages)
    {
       
        if (!_encounterMessageEnabled)
        {


            _currentEncounterMessages.Clear();
            _currentMessageIndex = 0;
            _encounterMessage.transform.parent.gameObject.SetActive(true);
            _currentEncounterMessages.AddRange(messages);
            _encounterMessage.text = _currentEncounterMessages[_currentMessageIndex];
            _encounterMessageEnabled = true;
            _currentMessageIndex++;
        }
        else
        {
            _currentEncounterMessages.AddRange(messages);

        }

        Image messageBox = _encounterMessage.transform.parent.GetComponent<Image>();
        messageBox.color = new Color(1f, 1f, 1f, 0.5f);
        Text emotionCoordinate = GameObject.Find("EncounterMessage/EmotionCoordinate").GetComponent<Text>();
        emotionCoordinate.text = "";
    }

    private void HideEncounterMessage()
    {
        _encounterMessage.transform.parent.gameObject.SetActive(false);
        _encounterMessageEnabled = false;
    }

    #endregion Display Encounter Message Methods

    public void ShowSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(true);
    }

    private void HideSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(false);
    }

    private void ResetMenu()
    {
        switch (_activeMenu)
        {
            case EncounterMenus.SSKILLSUBMENU:
                HideSubMenu(_sSkillSubMenu[sSkillSubMenuIndex].transform.parent.gameObject);
                sSkillSubMenuIndex = -1;
                break;
            case EncounterMenus.ESKILLSUBMENU:
                HideSubMenu(_eSkillSubMenu[eSkillSubMenuIndex].transform.parent.gameObject);
                eSkillSubMenuIndex = -1;
                break;
            case EncounterMenus.TSKILLSUBMENU:
                HideSubMenu(_tSkillSubMenu[tSkillSubMenuIndex].transform.parent.gameObject);
                tSkillSubMenuIndex = -1;
                break;
            case EncounterMenus.MSKILLSUBMENU:
                HideSubMenu(_mSkillSubMenu[mSkillSubMenuIndex].transform.parent.gameObject);
                mSkillSubMenuIndex = -1;
                break;
            case EncounterMenus.CSKILLSUBMENU:
                HideSubMenu(_cSkillSubMenu[cSkillSubMenuIndex].transform.parent.gameObject);
                cSkillSubMenuIndex = -1;
                break;
            default:
                Debug.LogWarning("This button shouldn't exist");
                break;
        }

        _activeMenu = EncounterMenus.BASEMENU;

        //MASON: look at this
        try
        {
            HideSubMenu(_skillSubMenu[skillSubMenuIndex].transform.parent.gameObject);
        }
        catch
        {
            Debug.Log("I believe " + _skillSubMenu + " is null...");
        }
    }

    private int SkipButton(int oldMenuIndex, int newMenuIndex, int menuCount, int lastOldIndex)
    {
        int menuMax = menuCount - 1;

        if (newMenuIndex == 0 && oldMenuIndex == menuMax && (lastOldIndex == menuMax - 1 || lastOldIndex == -1))
        {
            return 1;
        }
        else if (newMenuIndex == menuMax && oldMenuIndex == 0)
        {
            return menuMax - 1;
        }
        else if (newMenuIndex == 0 && oldMenuIndex > newMenuIndex)
        {
            return menuMax;
        }
        else if (newMenuIndex == menuMax && oldMenuIndex < newMenuIndex)
        {
            return 0;
        }
        else if (oldMenuIndex < newMenuIndex)
        {
            return newMenuIndex + 1;
        }
        else if (oldMenuIndex > newMenuIndex)
        {
            return newMenuIndex - 1;
        }
        else
        {
            return -1;
        }
    }

    private void EncounterInfoInitializer()
    {
        EncounterGoals.AddRange(CurrentEncounter.EncounterGoals);

        for (int i = 0; i < 4; i++)
        {
            if (i <= CurrentEncounter.GoalCount - 1)
            {
                if (i == 0)
                {
                    _targetName1.text = EncounterGoals[i].Subject.ToUpper();
                    _treatment1.text = EncounterGoals[i].ActionName;

                    //checkboxes
                    if (EncounterGoals[i].TreatmentCount == 1)
                    {
                        _target1CB1.gameObject.SetActive(false);
                        _target1CB2.gameObject.SetActive(false);
                    }
                    if (EncounterGoals[i].TreatmentCount == 2)
                    {
                        _target1CB1.gameObject.SetActive(false);
                    }

                    //trust
                    target1CurrentTrust = EncounterGoals[i].InitialTrust;

                    //Spawn target
                    _targetSpawnPoint = GameObject.Find("SpawnPoints/" + EncounterGoals[i].Subject).transform;
                    GameObject characterToSpawn = Resources.Load<GameObject>("Prefabs/NPCs/" + EncounterGoals[i].Subject);
                    if (characterToSpawn)
                        Instantiate(characterToSpawn, _targetSpawnPoint.position, _targetSpawnPoint.transform.rotation);
                }
                if (i == 1)
                {
                    _targetName2.text = EncounterGoals[i].Subject.ToUpper();
                    _treatment2.text = EncounterGoals[i].ActionName;

                    if (EncounterGoals[i].TreatmentCount == 1)
                    {
                        _target2CB1.gameObject.SetActive(false);
                        _target2CB2.gameObject.SetActive(false);
                    }
                    if (EncounterGoals[i].TreatmentCount == 2)
                    {
                        _target2CB1.gameObject.SetActive(false);
                    }

                    //trust
                    target2CurrentTrust = EncounterGoals[i].InitialTrust;
                }
                if (i == 2)
                {
                    _targetName3.text = EncounterGoals[i].Subject.ToUpper();
                    _treatment3.text = EncounterGoals[i].ActionName;

                    if (EncounterGoals[i].TreatmentCount == 1)
                    {
                        _target3CB1.gameObject.SetActive(false);
                        _target3CB2.gameObject.SetActive(false);
                    }
                    if (EncounterGoals[i].TreatmentCount == 2)
                    {
                        _target3CB1.gameObject.SetActive(false);
                    }

                    //trust
                    target3CurrentTrust = EncounterGoals[i].InitialTrust;
                }
                if (i == 3)
                {
                    _targetName4.text = EncounterGoals[i].Subject.ToUpper();
                    _treatment4.text = EncounterGoals[i].ActionName;

                    if (EncounterGoals[i].TreatmentCount == 1)
                    {
                        _target4CB1.gameObject.SetActive(false);
                        _target4CB2.gameObject.SetActive(false);
                    }
                    if (EncounterGoals[i].TreatmentCount == 2)
                    {
                        _target4CB1.gameObject.SetActive(false);
                    }

                    //trust
                    target4CurrentTrust = EncounterGoals[i].InitialTrust;
                }
            }
            else
            {
                if (i == 1)
                    GameObject.Find("Target 2").SetActive(false);
                if (i == 2)
                    GameObject.Find("Target 3").SetActive(false);
                if (i == 3)
                    GameObject.Find("Target 4").SetActive(false);
            }
        }
    }

    private void PopulateTypeSkillSubMenu()
    {
        for (int i = 0; i < _theGameManager.CurrentSimoneSkills.Count; i++)
        {
            switch (_theGameManager.CurrentSimoneSkills[i].MySkillType)
            {
                case SkillType.SCIENCE:
                    _sSkillCount++;

                    if (_sSkillCount == 1)
                        _sSkill1.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_sSkillCount == 2)
                        _sSkill2.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_sSkillCount == 3)
                        _sSkill3.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    break;
                case SkillType.ENGINEERING:
                    _eSkillCount++;

                    if (_eSkillCount == 1)
                        _eSkill1.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_eSkillCount == 2)
                        _eSkill2.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_eSkillCount == 3)
                        _eSkill3.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    break;
                case SkillType.TECHNOLOGY:
                    _tSkillCount++;

                    if (_tSkillCount == 1)
                        _tSkill1.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_tSkillCount == 2)
                        _tSkill2.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_tSkillCount == 3)
                        _tSkill3.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    break;
                case SkillType.MATHAMATICS:
                    _mSkillCount++;

                    if (_mSkillCount == 1)
                        _mSkill1.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_mSkillCount == 2)
                        _mSkill2.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_mSkillCount == 3)
                        _mSkill3.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    break;
                case SkillType.COMMUNICATION:
                    _cSkillCount++;

                    if (_cSkillCount == 1)
                        _cSkill1.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_cSkillCount == 2)
                        _cSkill2.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    if (_cSkillCount == 3)
                        _cSkill3.GetComponentInChildren<Text>().text = _theGameManager.CurrentSimoneSkills[i].Name;
                    break;
                default:
                    Debug.Log("Wait, check that skill type. Something is wrong here.");
                    break;
            }
        }

        if (_sSkillCount == 0)
        {
            _science.interactable = false;
            _science.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            _sSkill1.gameObject.SetActive(false);
            _sSkill2.gameObject.SetActive(false);
            _sSkill3.gameObject.SetActive(false);
        }
        else if (_sSkillCount == 1)
        {
            _sSkillSubMenu.Add(_sSkill1);
            _sSkill2.gameObject.SetActive(false);
            _sSkill3.gameObject.SetActive(false);
        }
        else if (_sSkillCount == 2)
        {
            _sSkillSubMenu.Add(_sSkill1);
            _sSkillSubMenu.Add(_sSkill2);
            _sSkill3.gameObject.SetActive(false);
        }
        else if (_sSkillCount == 3)
        {
            _sSkillSubMenu.Add(_sSkill1);
            _sSkillSubMenu.Add(_sSkill2);
            _sSkillSubMenu.Add(_sSkill3);
        }

        if (_eSkillCount == 0)
        {
            _engineering.interactable = false;
            _engineering.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            _eSkill1.gameObject.SetActive(false);
            _eSkill2.gameObject.SetActive(false);
            _eSkill3.gameObject.SetActive(false);
        }
        else if (_eSkillCount == 1)
        {
            _eSkillSubMenu.Add(_eSkill1);
            _eSkill2.gameObject.SetActive(false);
            _eSkill3.gameObject.SetActive(false);
        }
        else if (_eSkillCount == 2)
        {
            _eSkillSubMenu.Add(_eSkill1);
            _eSkillSubMenu.Add(_eSkill2);
            _eSkill3.gameObject.SetActive(false);
        }
        else if (_eSkillCount == 3)
        {
            _eSkillSubMenu.Add(_eSkill1);
            _eSkillSubMenu.Add(_eSkill2);
            _eSkillSubMenu.Add(_eSkill3);
        }

        if (_tSkillCount == 0)
        {
            _technology.interactable = false;
            _technology.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            _tSkill1.gameObject.SetActive(false);
            _tSkill2.gameObject.SetActive(false);
            _tSkill3.gameObject.SetActive(false);
        }
        else if (_tSkillCount == 1)
        {
            _tSkillSubMenu.Add(_tSkill1);
            _tSkill2.gameObject.SetActive(false);
            _tSkill3.gameObject.SetActive(false);
        }
        else if (_tSkillCount == 2)
        {
            _tSkillSubMenu.Add(_tSkill1);
            _tSkillSubMenu.Add(_tSkill2);
            _tSkill3.gameObject.SetActive(false);
        }
        else if (_tSkillCount == 3)
        {
            _tSkillSubMenu.Add(_tSkill1);
            _tSkillSubMenu.Add(_tSkill2);
            _tSkillSubMenu.Add(_tSkill3);
        }

        if (_mSkillCount == 0)
        {
            _mathamatics.interactable = false;
            _mathamatics.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            _mSkill1.gameObject.SetActive(false);
            _mSkill2.gameObject.SetActive(false);
            _mSkill3.gameObject.SetActive(false);
        }
        else if (_mSkillCount == 1)
        {
            _mSkillSubMenu.Add(_mSkill1);
            _mSkill2.gameObject.SetActive(false);
            _mSkill3.gameObject.SetActive(false);
        }
        else if (_mSkillCount == 2)
        {
            _mSkillSubMenu.Add(_mSkill1);
            _mSkillSubMenu.Add(_mSkill2);
            _mSkill3.gameObject.SetActive(false);
        }
        else if (_mSkillCount == 3)
        {
            _mSkillSubMenu.Add(_mSkill1);
            _mSkillSubMenu.Add(_mSkill2);
            _mSkillSubMenu.Add(_mSkill3);
        }

        if (_cSkillCount == 0)
        {
            _communication.interactable = false;
            _communication.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            _cSkill1.gameObject.SetActive(false);
            _cSkill2.gameObject.SetActive(false);
            _cSkill3.gameObject.SetActive(false);
        }
        else if (_cSkillCount == 1)
        {
            _cSkillSubMenu.Add(_cSkill1);
            _cSkill2.gameObject.SetActive(false);
            _cSkill3.gameObject.SetActive(false);
        }
        else if (_cSkillCount == 2)
        {
            _cSkillSubMenu.Add(_cSkill1);
            _cSkillSubMenu.Add(_cSkill2);
            _cSkill3.gameObject.SetActive(false);
        }
        else if (_cSkillCount == 3)
        {
            _cSkillSubMenu.Add(_cSkill1);
            _cSkillSubMenu.Add(_cSkill2);
            _cSkillSubMenu.Add(_cSkill3);
        }
    }
}