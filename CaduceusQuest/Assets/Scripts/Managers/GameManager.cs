using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager TheGameManager;

    public List<Event> AllEvents = new List<Event>();

    public Dictionary<string, char> CurrentDialogIndexList
    {
        get
        {
            return _currentDialogIndexList;
        }
    }
    private static Dictionary<string, char> _currentDialogIndexList = new Dictionary<string, char>();
    private static Dictionary<string, int> _currentEncounterIndexList = new Dictionary<string, int>();
    private static List<NPC> _lastNPCList = new List<NPC>();

    private static Dictionary<string, string> _sceneCameras = new Dictionary<string, string>();
    private static List<string> _unlockTriggerStrings = new List<string>(),
                                _lockTriggerStrings = new List<string>();
    private static string _completedEvent = "";

    private static Vector3 _lastPosition;
    public Vector3 LastPosition
    {
        get
        {
            return _lastPosition;
        }

        set
        {
            _lastPosition = value;
        }
    }

    private static Encounter _currentEncounter;
    public List<Event> CurrentEvents
    {
        get
        {
            return _currentEvents;
        }
    }
    private static List<Event> _currentEvents = new List<Event>();

    public bool FrontDoorGoesToOverworld
    {
        get
        {
            return _frontDoorGoesToOverworld;
        }

        set
        {
            if (value != _frontDoorGoesToOverworld)
            {
                GameObject actualDoor = GameObject.Find("DoorToAshland/ActualDoor"),
                           doorDialog = GameObject.Find("DoorToAshland/DoorDialog");
                if(value)
                {
                    actualDoor.GetComponent<DoorController>().enabled = true;
                    doorDialog.GetComponent<NPCDialogSwitch>().enabled = false;
                }
                else
                {
                    actualDoor.GetComponent<DoorController>().enabled = false;
                    doorDialog.GetComponent<NPCDialogSwitch>().enabled = true;
                }

                _frontDoorGoesToOverworld = value;
            }
        }
    }
    private bool _frontDoorGoesToOverworld;

    private static List<Skill> _currentSimoneSkills = new List<Skill>();
    public List<Skill> CurrentSimoneSkills //Public accessor fro static skills list
    {
        get
        {
            return _currentSimoneSkills;
        }
    }

    private EncounterManager _theEncounterManager;
    private CameraManager _theCamMan;

    private void Awake()
    {
        if (!TheGameManager)
            TheGameManager = this;
        //Should only happen the first time entering that scene
        List<NPCDialogSwitch> allNPCSwitches = new List<NPCDialogSwitch>();
        allNPCSwitches.AddRange(FindObjectsOfType<NPCDialogSwitch>());
        if (_currentEvents.Count == 0)
        {
            _currentEvents.Add(AllEvents[0]);
            for (int i = 0; i < AllEvents.Count; i++)
            {
                for (int j = 0; j < AllEvents[i].EventGoals.Count; j++)
                {
                    AllEvents[i].EventGoals[j].Achieved = false;
                }
            }
        }

        if (allNPCSwitches.Count > 0)
        {
            _lastNPCList.Clear();

            foreach (NPCDialogSwitch npc in allNPCSwitches)
            {
                _lastNPCList.Add(npc.NPCData);

                if (!_currentDialogIndexList.ContainsKey(npc.transform.parent.name))
                {
                    _currentDialogIndexList.Add(npc.transform.parent.name, 'a');
                }
            }
        }

        //AddSkill(new Skill("Complete Intake Form", SkillType.COMMUNICATION));
        //AddSkill(new Skill("Examine Neck", SkillType.SCIENCE));
        //AddSkill(new Skill("Collect Blood Sample", SkillType.SCIENCE));

        _theEncounterManager = FindObjectOfType<EncounterManager>();
        if (_theEncounterManager != null && _currentEncounter != null)
        {
            _theEncounterManager.CurrentEncounter = _currentEncounter;
        }

        if ((SceneManager.GetActiveScene().name == "School1" ||
           SceneManager.GetActiveScene().name == "School2" ||
           SceneManager.GetActiveScene().name == "Hospital") && LastPosition != Vector3.zero)
        {
            FindObjectOfType<SimoneController>().transform.position = LastPosition;
        }

        _theCamMan = FindObjectOfType<CameraManager>();

        if (_sceneCameras.Count == 0)
        {
            _sceneCameras.Add(SceneManager.GetActiveScene().name, "MainCamera1");
        }
        else
        {
            string activeScene = SceneManager.GetActiveScene().name;
            if (_sceneCameras.ContainsKey(activeScene))
            {
                Camera newCam = GameObject.Find(_sceneCameras[activeScene]).GetComponent<Camera>();
                _theCamMan.ActivateCam(newCam);
            }
        }
    }

    private void Start()
    {
        //Lets make this a methods
        if(_completedEvent != "")
        {
            EventCompletionMessage();

            if (_completedEvent == "Collect Blood Sample")
            {
                FrontDoorGoesToOverworld = true;
            }
        }
        else
        {
            FrontDoorGoesToOverworld = false;
        }

        //World spacific changes methods
        if (SceneManager.GetActiveScene().name == "Hospital")
        {
            if (_unlockTriggerStrings.Count != 0 && _lockTriggerStrings.Count != 0)
                UpdateTriggers();
        }
    }

    private void Update()
    {
        //HACK//
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            CurrentEvents.ForEach(delegate (Event theEvent) { theEvent.AssessEventGoals(); });
        }
    }

    public void UpdateDialogIndexList(string npcName, char newIndex)
    {
        _currentDialogIndexList[npcName] = newIndex;
    }

    public void AddSkill(string skillName, EncounterActionType actionType)
    {
        SkillType st = SkillType.COMMUNICATION;

        if (actionType == EncounterActionType.COMPSCI)
            st = SkillType.COMMUNICATION;
        else if (actionType == EncounterActionType.DOCTOR)
            st = SkillType.SCIENCE;
        else
            Debug.LogError("We don't have actions like that yet...");

        Skill newSkill = new Skill(skillName, st);

        if (!_currentSimoneSkills.Exists(x => x.Name == newSkill.Name))
            _currentSimoneSkills.Add(newSkill);
    }

    public void BeginEncounter(string encounterToLoadPath)
    {
        _currentEncounter = Resources.Load<Encounter>(encounterToLoadPath);

        string sceneType = "",
               rawSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < rawSceneName.Length; i++)
        {
            if (rawSceneName[i] == 1 || rawSceneName[i] == 2)
            {
                break;
            }
            else
                sceneType = sceneType + rawSceneName[i];
        }

        _sceneCameras[SceneManager.GetActiveScene().name] = _theCamMan.CurrentCamera.name;

        SceneManager.LoadScene("Scene/" + sceneType + "Encounter");
    }

    public void DialogueChanger(string name, DialogChangeType dct)
    {
        for (int i = 0; i < _lastNPCList.Count; i++)
        {
            if (_lastNPCList[i].NPCName == name)
            {
                for (int j = 0; j < _lastNPCList[i].NextConvoInfo.Length; j++)
                {
                    if (_currentDialogIndexList[name] == _lastNPCList[i].NextConvoInfo[j].MyIndex)
                    {
                        if (dct == _lastNPCList[i].NextConvoInfo[j].MyChangeType)
                        {
                            _currentDialogIndexList[name] = _lastNPCList[i].NextConvoInfo[j].NextIndex;
                            break;
                        }
                    }
                }
            }
        }
    }

    //In World events
    public void SetWorldTriggersToChange(List<string> onTriggers, List<string> offTriggers)
    {
        _unlockTriggerStrings.Clear();
        _lockTriggerStrings.Clear();
        _unlockTriggerStrings.AddRange(onTriggers);
        _lockTriggerStrings.AddRange(offTriggers);
    }

    private void UpdateTriggers()
    {
        if (_unlockTriggerStrings.Count > 0)
        {
            for (int i = 0; i < _unlockTriggerStrings.Count; i++)
            {
                GameObject.Find(_unlockTriggerStrings[i]).GetComponent<Collider>().enabled = true;
            }
        }

        if (_lockTriggerStrings.Count > 0)
        {
            for (int i = 0; i < _lockTriggerStrings.Count; i++)
            {
                GameObject.Find(_lockTriggerStrings[i]).GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void EventDialogueChanger(List<string> otherNames, List<char> nextChars)
    {
        for (int i = 0; i < otherNames.Count; i++)
        {
            _currentDialogIndexList[otherNames[i]] = nextChars[i];
        }
        //for (int i = 0; i < _lastNPCList.Count; i++)
        //{
        //    //if (_lastNPCList[i].NPCName == name)
        //    //{
        //    //    for (int j = 0; j < _lastNPCList[i].NextConvoInfo.Length; j++)
        //    //    {
        //    //        if (_currentDialogIndexList[name] == _lastNPCList[i].NextConvoInfo[j].MyIndex)
        //    //        {
        //    //            if (DialogChangeType.CONVOEND == _lastNPCList[i].NextConvoInfo[j].MyChangeType)
        //    //            {
        //    //                _currentDialogIndexList[name] = _lastNPCList[i].NextConvoInfo[j].NextIndex;
        //    //                break;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else 
        //    if (otherNames.Contains(_lastNPCList[i].NPCName))
        //    {
        //        for (int j = 0; j < _lastNPCList[i].NextConvoInfo.Length; j++)
        //        {
        //            if(dct == _lastNPCList[i].NextConvoInfo[j].MyChangeType)
        //            {
        //                _currentDialogIndexList[_lastNPCList[i].NPCName] = _lastNPCList[i].NextConvoInfo[j].NextIndex;
        //                break;
        //            }
        //        }
        //    }
        //}
    }

    //public void SetEventsMessage()
    //{
    //    _completedEvent = CurrentEvent.EventGoals[0].Treatment;
    //}

    private void EventCompletionMessage()
    {
        DialogueUIController dialogController = FindObjectOfType<DialogueUIController>();

        dialogController.ShowEventMessage(_completedEvent);
        _completedEvent = "";
    }

    public void ProceedToNextEvent(Event oldEvent)
    {
        List<Event> newEvents = new List<Event>();
        if (oldEvent.NextEvents.Count > 0)
        {
            for (int i = 0; i < oldEvent.NextEvents.Count; i++)
            {
                newEvents.Add(oldEvent.NextEvents[i]);
            }
        }

        _currentEvents.Remove(oldEvent);
        _currentEvents.AddRange(newEvents);
    }
}