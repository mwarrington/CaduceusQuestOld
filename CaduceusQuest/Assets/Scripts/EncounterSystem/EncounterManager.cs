using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGoal> EncounterGoals = new List<EncounterGoal>();
    public List<EncounterController> EncounterControllers = new List<EncounterController>();
    public Sprite DefalutButtonSprite, CheckedBox;
    public Sprite[] TrustProgressBarSprites;
    public Canvas EntireMenu;

    private GameManager _theGameManager;
    private Image _target1CB1, _target1CB2, _target1CB3,
                  _target2CB1, _target2CB2, _target2CB3,
                  _target3CB1, _target3CB2, _target3CB3,
                  _target4CB1, _target4CB2, _target4CB3,
                  _target1Trust, _target2Trust, _target3Trust, _target4Trust;
    private Text _targetName1, _targetName2, _targetName3, _targetName4,
                 _treatment1, _treatment2, _treatment3, _treatment4;
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

    private EncounterMenus _activeMenu = EncounterMenus.BASEMENU;
    private bool _inputEnabled = true;

    #region Menu Index Properties
    //These handle image and text color change for menu navigation
    protected int baseMenuIndex
    {
        get { return _baseMenuIndex; }

        set
        {
            if(_baseMenuIndex != value)
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
            if(_target1CurrentTrust != value)
            {
                if (value == 0)
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
                else if (value > 90)
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
                else if (value > 90)
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
                else if (value > 90)
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
                else if (value > 90)
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

        EncounterInfoInitializer();
        #endregion Getting/Setting Encounter Data
    }

    private void CreateTurnOrder()
    {
        //EncounterControllers.Add(???)
    }

    private void Update()
    {
        if (_inputEnabled)
            UIInputHandler();
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

            if(currentIndex < 0)
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
            currentMenu[currentIndex].onClick.Invoke();

            switch(currentMenu[currentIndex].name)
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
        else if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Make can't do that sound
        }

        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _activeMenu = EncounterMenus.BASEMENU;

            switch (currentMenu[currentIndex].name)
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
                            Debug.LogWarning("This button shouldn't exist");
                            break;
                    }
                    break;
            }
            
            HideSubMenu(currentMenu[currentIndex].transform.parent.gameObject);
        }
        #endregion Horizontal Menu Naviagion and Select Controls
    }

    public void LoadMiniGame(Text myText)
    {
        string skillName = myText.text;
        
        EncounterAction ea = Resources.Load<EncounterAction>("EncounterActions/" + skillName);

        switch (ea.MyType)
        {
            case EncounterActionType.COMPSCI:
                EncounterActionCompSci myCompSciSO = (EncounterActionCompSci)ea;
                GameObject CompSciEAObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/CompSciPuzzle/CompSciPuzzle" + myCompSciSO.SymbolCount);
                CompSciEAObj = GameObject.Instantiate(CompSciEAObj);
                CompSciPuzzleManager cspm = CompSciEAObj.GetComponent<CompSciPuzzleManager>();
                cspm.Strikes = myCompSciSO.StrikeCount;
                cspm.Name = myCompSciSO.Name;
                cspm.FailPenalty = myCompSciSO.FailPenalty;
                ToggleWholeMenu();
                break;
            case EncounterActionType.DOCTOR:
                EncounterActionDoctor myDoctorSO = (EncounterActionDoctor)ea;
                GameObject DoctorPuzzleObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/Doctor/DoctorPuzzleDefault");
                DoctorPuzzleObj = GameObject.Instantiate(DoctorPuzzleObj, new Vector3(this.transform.position.x - 1000f, this.transform.position.y - 1000f, this.transform.position.z), Quaternion.identity);
                DoctorPuzzleManager myDocPuzzMan = DoctorPuzzleObj.GetComponent<DoctorPuzzleManager>();
                myDocPuzzMan.Name = myDoctorSO.Name;
                myDocPuzzMan.FailPenalty = myDoctorSO.FailPenalty;
                myDocPuzzMan.KeyStrokeCount = myDoctorSO.KeyStrokeCount;
                myDocPuzzMan.MinArrowSpeed = myDoctorSO.ArrowMinSpeed;
                myDocPuzzMan.MaxArrowSpeed = myDoctorSO.ArrowMaxSpeed;
                myDocPuzzMan.SpawnRate = myDoctorSO.SpawnRate;
                ToggleWholeMenu();
                break;
            case EncounterActionType.DIALOG:
                EncounterActionDialog myDialogSO = (EncounterActionDialog)ea;
                GameObject DialogPuzzleObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/Dialog/DialogPuzzle");
                DialogPuzzleObj = GameObject.Instantiate(DialogPuzzleObj);
                DialogPuzzleManager myDialogPuzzMan = DialogPuzzleObj.GetComponent<DialogPuzzleManager>();
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
    }

    public void PuzzleFail(float failPenalty, GameObject currentPuzzle)
    {
        ToggleWholeMenu();
        target1CurrentTrust -= failPenalty;
        GameObject.Destroy(currentPuzzle);
    }

    public void PuzzleWin(string actionName, GameObject currentPuzzle)
    {
        for (int i = 0; i < EncounterGoals.Count; i++)
        {
            if(EncounterGoals[i].ActionName == actionName)
            {
                if(i == 0)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target1SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target1CB3.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target1CB2.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target1CB1.sprite = CheckedBox;
                    }

                    _target1SuccessCount++;

                    if(_target1SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        //True win
                    }
                    else
                    {
                        ToggleWholeMenu();
                        GameObject.Destroy(currentPuzzle);
                    }
                }
                if (i == 1)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target2SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target2CB3.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target2CB2.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target2CB1.sprite = CheckedBox;
                    }

                    _target2SuccessCount++;

                    if (_target2SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        //True win
                    }
                    else
                    {
                        ToggleWholeMenu();
                        GameObject.Destroy(currentPuzzle);
                    }
                }
                if (i == 2)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target3SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target3CB3.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target3CB2.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target3CB1.sprite = CheckedBox;
                    }

                    _target3SuccessCount++;

                    if (_target3SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        //True win
                    }
                    else
                    {
                        ToggleWholeMenu();
                        GameObject.Destroy(currentPuzzle);
                    }
                }
                if (i == 3)
                {
                    int checkBoxIndex = EncounterGoals[i].TreatmentCount - _target4SuccessCount;

                    if (checkBoxIndex == 3)
                    {
                        _target4CB3.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 2)
                    {
                        _target4CB2.sprite = CheckedBox;
                    }
                    else if (checkBoxIndex == 1)
                    {
                        _target4CB1.sprite = CheckedBox;
                    }

                    _target4SuccessCount++;

                    if (_target3SuccessCount == EncounterGoals[i].TreatmentCount)
                    {
                        //True win
                    }
                    else
                    {
                        ToggleWholeMenu();
                        GameObject.Destroy(currentPuzzle);
                    }
                }
            }
        }

    }

    private void ToggleWholeMenu()
    {
        EntireMenu.gameObject.SetActive(!EntireMenu.gameObject.activeSelf);
        _inputEnabled = !_inputEnabled;
    }

    public void ShowSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(true);
    }
    private void HideSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(false);
    }

    private int SkipButton(int oldMenuIndex, int newMenuIndex, int menuCount, int lastOldIndex)
    {
        int menuMax = menuCount - 1;

        if (newMenuIndex == 0 && oldMenuIndex == menuMax && (lastOldIndex == menuMax - 1 || lastOldIndex == -1))
        {
            return 1;
        }
        else if(newMenuIndex == menuMax && oldMenuIndex == 0)
        {
            return menuMax - 1;
        }
        else if(newMenuIndex == 0 && oldMenuIndex > newMenuIndex)
        {
            return menuMax;
        }
        else if(newMenuIndex == menuMax && oldMenuIndex < newMenuIndex)
        {
            return 0;
        }
        else if(oldMenuIndex < newMenuIndex)
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
        EncounterGoals.AddRange(_theGameManager.CurrentEncounter.EncounterGoals);

        for (int i = 0; i < 4; i++)
        {
            if(i <= _theGameManager.CurrentEncounter.GoalCount - 1)
            {
                if(i == 0)
                {
                    _targetName1.text = EncounterGoals[i].Subject;
                    _treatment1.text = EncounterGoals[i].ActionName;

                    //checkboxes
                    if(EncounterGoals[i].TreatmentCount == 1)
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
                }
                if (i == 1)
                {
                    _targetName2.text = EncounterGoals[i].Subject;
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
                    _targetName3.text = EncounterGoals[i].Subject;
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
                    _targetName4.text = EncounterGoals[i].Subject;
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
            switch(_theGameManager.CurrentSimoneSkills[i].MySkillType)
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

        if(_sSkillCount == 0)
        {
            _science.interactable = false;
            _science.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            _sSkill1.gameObject.SetActive(false);
            _sSkill2.gameObject.SetActive(false);
            _sSkill3.gameObject.SetActive(false);
        }
        else if(_sSkillCount == 1)
        {
            _sSkillSubMenu.Add(_sSkill1);
            _sSkill2.gameObject.SetActive(false);
            _sSkill3.gameObject.SetActive(false);
        }
        else if(_sSkillCount == 2)
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