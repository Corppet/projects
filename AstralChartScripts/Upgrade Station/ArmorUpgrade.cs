using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Upgrade Object/Armor Upgrade", order = 1)]
public class ArmorUpgrade : UpgradeObject
{
    public ArmorUpgrade()
    {
        upgradeType = UpgradeType.Armor;
    }

    [Space]

    public float defense;

    public bool Equals(ArmorUpgrade other)
    {
        return this.upgradeName == other.upgradeName;
    }
}
