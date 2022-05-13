using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum WeaponType
{
    Directional, Controlled, Pressed
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Upgrade Object/Weapon Upgrade", order = 1)]
public class WeaponUpgrade : UpgradeObject
{
    public WeaponUpgrade()
    {
        upgradeType = UpgradeType.Weapon;
    }

    public WeaponType weaponType;
    public float damage;
    public float cooldown;

    [Space]

    public Sprite uiImage;
    public VideoClip previewClip;
    [Tooltip("The bounty that unlocks this weapon when claimed.")]
    public BountyObject bounty;
    [HideInInspector] public int weaponPoints;
    [HideInInspector] public WeaponUpgrade prerequisite;

    public bool treePurchased { get; set; } = false;

    public bool Equals(WeaponUpgrade other)
    { 
        return this.upgradeName == other.upgradeName; 
    }
}