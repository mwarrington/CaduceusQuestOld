using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<DialogueUIController> _allDialogControllers = new List<DialogueUIController>();
    public List<Skill> CurrentSimoneSkills = new List<Skill>();
    public Dictionary<string, int> CurrentEncounterIndexList = new Dictionary<string, int>();
    public Encounter CurrentEncounter;

    private void Awake()
    {
        _allDialogControllers.AddRange(FindObjectsOfType<DialogueUIController>());
        AddSkill(new Skill("Complete Intake Form", SkillType.COMMUNICATION));
        AddSkill(new Skill("Examine Neck", SkillType.SCIENCE));
        //AddSkill(new Skill("Collect Blood Sample", SkillType.SCIENCE));
    }

    public void AddSkill(Skill skillToAdd)
    {
        CurrentSimoneSkills.Add(skillToAdd);
    }
}