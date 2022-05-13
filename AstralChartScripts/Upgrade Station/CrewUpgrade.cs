using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crewmate", menuName = "Upgrade Object/Crewmate Upgrade", order = 1)]
public class CrewUpgrade : UpgradeObject
{
    public CrewUpgrade()
    {
        upgradeType = UpgradeType.Crewmate;
    }

    public bool piloting;
    public bool weaponry;
    public bool engineering;

    public bool Equals(CrewUpgrade other)
    {
        return this.upgradeName == other.upgradeName;
    }
}
