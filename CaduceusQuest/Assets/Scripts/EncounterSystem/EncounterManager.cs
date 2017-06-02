using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGoal> EncounterGoals = new List<EncounterGoal>();
    public List<EncounterController> EncounterControllers = new List<EncounterController>();
    public Sprite DefalutButtonSprite;

    private GameManager _theGameManager;
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
                    if (SkipButton(_baseMenuIndex, value, _baseMenu.Length) != -1)
                        baseMenuIndex = SkipButton(_baseMenuIndex, value, _baseMenu.Length);
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
                    if (SkipButton(_skillSubMenuIndex, value, _skillSubMenu.Length) != -1)
                        skillSubMenuIndex = SkipButton(_skillSubMenuIndex, value, _skillSubMenu.Length);
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
    private int _baseMenuIndex = 0, _skillSubMenuIndex = -1, _sSkillSubMenuIndex = -1, _eSkillSubMenuIndex = -1, _tSkillSubMenuIndex = -1, _mSkillSubMenuIndex = -1, _cSkillSubMenuIndex = -1;
    #endregion Menu Index Properties

    private int _sSkillCount, _eSkillCount, _tSkillCount, _mSkillCount, _cSkillCount;

    private void Start()
    {
        _theGameManager = FindObjectOfType<GameManager>();

        #region Finding Encounter Buttons
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
        #endregion Finding Encounter Buttons
    }

    private void CreateTurnOrder()
    {
        //EncounterControllers.Add(???)
    }

    private void Update()
    {
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

    public void ShowSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(true);
    }
    private void HideSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(false);
    }

    private int SkipButton(int oldMenuIndex, int newMenuIndex, int menuCount)
    {
        int menuMax = menuCount - 1;

        if (newMenuIndex == 0 && oldMenuIndex == menuMax)
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