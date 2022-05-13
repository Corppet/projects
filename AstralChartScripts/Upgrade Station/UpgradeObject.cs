using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Weapon,
    Armor,
    Mainframe,
    Crewmate,
    Skill
};

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade Object", order = 2)]
public class UpgradeObject : ScriptableObject
{
    public UpgradeType upgradeType;

    public string upgradeName;
    [TextArea(5, 20)] public string description;

    public int cost;

    public GameObject upgradePrefab;

    public bool Equals(UpgradeObject other)
    {
        return this.upgradeName == other.upgradeName;
    }
}