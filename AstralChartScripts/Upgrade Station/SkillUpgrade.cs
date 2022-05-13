using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Skill", menuName = "Upgrade Object/Skill Upgrade", order = 1)]
public class SkillUpgrade : UpgradeObject
{
    public SkillUpgrade()
    {
        upgradeType = UpgradeType.Skill;
    }

    public int rating;
    public int reputation;

    public bool Equals(SkillUpgrade other)
    {
        return this.upgradeName == other.upgradeName;
    }
}
