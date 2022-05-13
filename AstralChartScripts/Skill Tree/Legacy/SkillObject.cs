using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "SkillObject", menuName = "Skill Tree/SkillObject", order = 0)]
public class SkillObject : ScriptableObject {
    public char faction;
    public int points;
    public bool locked;

    public SkillObject prerequisite;
}