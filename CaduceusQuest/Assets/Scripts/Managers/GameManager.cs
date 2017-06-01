using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<DialogueUIController> _allDialogControllers = new List<DialogueUIController>();
    public List<Skill> CurrentSimoneSkills = new List<Skill>();

    private void Start()
    {
        _allDialogControllers.AddRange(FindObjectsOfType<DialogueUIController>());
        AddSkill(new Skill("Complete Intake Form", SkillType.COMMUNICATION));
        AddSkill(new Skill("Examine Neck", SkillType.SCIENCE));
        AddSkill(new Skill("Collect Blood Sample", SkillType.SCIENCE));
    }

    public void AddSkill(Skill skillToAdd)
    {
        CurrentSimoneSkills.Add(skillToAdd);
    }
}