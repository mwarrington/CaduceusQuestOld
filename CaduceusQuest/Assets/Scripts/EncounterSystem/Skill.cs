using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string Name;
    public SkillType MySkillType;

    public Skill(string name, SkillType skillType)
    {
        Name = name;
        MySkillType = skillType;
    }
}