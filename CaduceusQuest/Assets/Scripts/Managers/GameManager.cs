﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private static Encounter _currentEncounter;
    public Event CurrentEvent
    {
        get
        {
            return _currentEvent;
        }
    }
    private static Event _currentEvent;

    public List<Skill> CurrentSimoneSkills = new List<Skill>();

    private EncounterManager _theEncounterManager;

    private void Awake()
    {
        //Should only happen the first time entering that scene
        List<NPCDialogSwitch> allNPCSwitches = new List<NPCDialogSwitch>();
        allNPCSwitches.AddRange(FindObjectsOfType<NPCDialogSwitch>());
        if (!_currentEvent)
            _currentEvent = Resources.Load<Event>("Events/Event1");

        if (allNPCSwitches.Count > 0)
        {
            _lastNPCList.Clear();

            foreach (NPCDialogSwitch npc in allNPCSwitches)
            {
                _lastNPCList.Add(npc.NPCData);

                if (!_currentDialogIndexList.ContainsKey(npc.transform.parent.name))
                    _currentDialogIndexList.Add(npc.transform.parent.name, 'a');
            }
        }

        AddSkill(new Skill("Complete Intake Form", SkillType.COMMUNICATION));
        AddSkill(new Skill("Examine Neck", SkillType.SCIENCE));
        //AddSkill(new Skill("Collect Blood Sample", SkillType.SCIENCE));

        _theEncounterManager = FindObjectOfType<EncounterManager>();
        if(_theEncounterManager != null && _currentEncounter != null)
        {
            _theEncounterManager.CurrentEncounter = _currentEncounter;
        }
    }

    public void UpdateDialogIndexList(string npcName, char newIndex)
    {
        _currentDialogIndexList[npcName] = newIndex;
    }

    public void AddSkill(Skill skillToAdd)
    {
        CurrentSimoneSkills.Add(skillToAdd);
    }

    public void BeginEncounter(string sceneToLoadPath)
    {
        _currentEncounter = Resources.Load<Encounter>(sceneToLoadPath);

        string sceneType = "",
               rawSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < rawSceneName.Length; i++)
        {
            if (rawSceneName[i] == 1 || rawSceneName[i] != 2)
            {
                break;
            }
            else
                sceneType = sceneType + rawSceneName[i];
        }

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
}