using System.Collections;
using System.Collections.Generic;
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

    public List<Skill> CurrentSimoneSkills = new List<Skill>();
    public Encounter CurrentEncounter;

    private void Awake()
    {
        //Should only happen the first time entering that scene
        foreach (NPCDialogSwitch npc in FindObjectsOfType<NPCDialogSwitch>())
        {
            _currentDialogIndexList.Add(npc.transform.parent.name, 'a');
        }

        AddSkill(new Skill("Complete Intake Form", SkillType.COMMUNICATION));
        AddSkill(new Skill("Examine Neck", SkillType.SCIENCE));
        //AddSkill(new Skill("Collect Blood Sample", SkillType.SCIENCE));
    }

    public void UpdateDialogIndexList(string npcName, char newIndex)
    {
        _currentDialogIndexList[npcName] = newIndex;
    }

    public void AddSkill(Skill skillToAdd)
    {
        CurrentSimoneSkills.Add(skillToAdd);
    }
}