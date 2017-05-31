using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGoal> EncounterGoals = new List<EncounterGoal>();
    public List<EncounterController> EncounterControllers = new List<EncounterController>();

    private Button _skills, _items, _endEncounter,
                   _science, _engineering, _technology, _mathamatics, _communication,
                   _sSkill1, _sSkill2, _sSkill3,
                   _eSkill1, _eSkill2, _eSkill3,
                   _tSkill1, _tSkill2, _tSkill3,
                   _mSkill1, _mSkill2, _mSkill3,
                   _cSkill1, _cSkill2, _cSkill3;

    private Button[] _baseMenu = new Button[3],
                     _skillSubMenu = new Button[5];
    private List<Button> _scienceSubMenu = new List<Button>(),
                         _engineeringSubMenu = new List<Button>(),
                         _technologySubMenu = new List<Button>(),
                         _mathamaticsSubMenu = new List<Button>(),
                         _communicationSubMenu = new List<Button>();

    private EncounterMenus _activeMenu = EncounterMenus.BASEMENU;

    private int _baseMenuIndex = 0,
                _skillSubMenuIndex = -1,
                _sSkillSubMenuIndex = -1,
                _eSkillSubMenuIndex = -1,
                _tSkillSubMenuIndex = -1,
                _mSkillSubMenuIndex = -1,
                _cSkillSubMenuIndex = -1;

    private void Start()
    {
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
        _tSkill2 = GameObject.Find("Technology Skill 2").GetComponent<Button>();
        _tSkill3 = GameObject.Find("Technology Skill 3").GetComponent<Button>();
        _mSkill1 = GameObject.Find("Mathamatics Skill 1").GetComponent<Button>();
        _mSkill3 = GameObject.Find("Mathamatics Skill 3").GetComponent<Button>();
        _cSkill1 = GameObject.Find("Communication Skill 1").GetComponent<Button>();
        _cSkill2 = GameObject.Find("Communication Skill 2").GetComponent<Button>();
        _cSkill3 = GameObject.Find("Communication Skill 3").GetComponent<Button>();

        _baseMenu[0] = _skills;
        _baseMenu[1] = _items;
        _baseMenu[2] = _endEncounter;

        _skillSubMenu[0] = _science;
        _skillSubMenu[1] = _engineering;
        _skillSubMenu[2] = _technology;
        _skillSubMenu[3] = _mathamatics;
        _skillSubMenu[4] = _communication;
        #endregion Finding Encounter Buttons


    }

    //public GameObject[] SubMenus;

    private void CreateTurnOrder()
    {
        //EncounterControllers.Add(???)
    }

    private void OnGUI()
    {
        //UIInputHandler();
    }

    //private void UIInputHandler()
    //{
    //    List<Button> currentMenu = new List<Button>();
    //    int currentIndex = -2;

    //    switch (_activeMenu)
    //    {
    //        case EncounterMenus.BASEMENU:
    //            currentMenu.AddRange(_baseMenu);
    //            currentIndex = baseMenuIndex;
    //            break;
    //        case EncounterMenus.SKILLSUBMENU:
    //            currentMenu.AddRange(_skillSubMenu);
    //            currentIndex = skillSubMenuIndex;
    //            break;
    //        case EncounterMenus.SSKILLSUBMENU:
    //            currentMenu.AddRange(_scienceSubMenu);
    //            currentIndex = sSkillSubMenuIndex;
    //            break;
    //        case EncounterMenus.ESKILLSUBMENU:
    //            currentMenu.AddRange(_engineeringSubMenu);
    //            currentIndex = eSkillSubMenuIndex;
    //            break;
    //        case EncounterMenus.TSKILLSUBMENU:
    //            currentMenu.AddRange(_technologySubMenu);
    //            currentIndex = tSkillSubMenuIndex;
    //            break;
    //        case EncounterMenus.MSKILLSUBMENU:
    //            currentMenu.AddRange(_mathamaticsSubMenu);
    //            currentIndex = mSkillSubMenuIndex;
    //            break;
    //        case EncounterMenus.CSKILLSUBMENU:
    //            currentMenu.AddRange(_communicationSubMenu);
    //            currentIndex = cSkillSubMenuIndex;
    //            break;
    //        default:
    //            break;
    //    }

    //    if(Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        currentIndex++;

    //        if(currentIndex == currentMenu.Count)
    //        {
    //            currentIndex = 0;
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {

    //    }

    //    switch (_activeMenu)
    //    {
    //        case EncounterMenus.BASEMENU:
    //            baseMenuIndex = currentIndex;
    //            break;
    //        case EncounterMenus.SKILLSUBMENU:
    //            skillSubMenuIndex = currentIndex;
    //            break;
    //        case EncounterMenus.SSKILLSUBMENU:
    //            sSkillSubMenuIndex = currentIndex;
    //            break;
    //        case EncounterMenus.ESKILLSUBMENU:
    //            eSkillSubMenuIndex = currentIndex;
    //            break;
    //        case EncounterMenus.TSKILLSUBMENU:
    //            tSkillSubMenuIndex = currentIndex;
    //            break;
    //        case EncounterMenus.MSKILLSUBMENU:
    //            mSkillSubMenuIndex = currentIndex;
    //            break;
    //        case EncounterMenus.CSKILLSUBMENU:
    //            cSkillSubMenuIndex = currentIndex;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void SetButtonActive(Button butt)
    {

    }

    public void ShowSubMenu(GameObject subMenu)
    {
        subMenu.SetActive(true);
    }
}