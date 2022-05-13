using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Selectable weapon in the shop.
/// </summary>
[RequireComponent(typeof(Button))]
public class WeaponButton : UpgradeButton
{
    // Other Serialized Fields
    [SerializeField] protected TMP_Text damageText;
    [SerializeField] protected TMP_Text cooldownText;
    [SerializeField] protected Image weaponImage;
    [SerializeField] protected Sprite lockSprite;
    [SerializeField] protected Color unequippedColor = new Color(255f, 251f, 78f);
    [SerializeField] protected Color equippedColor = new Color(255f, 99f, 78f);


    /// <summary>
    /// Update information displayed on the button.
    /// </summary>
    public void updateDetails()
    {
        WeaponUpgrade weaponDetails = (WeaponUpgrade)upgradeDetails;

        titleText.text = weaponDetails.upgradeName;
        if (equipped)
            titleText.color = equippedColor;
        else
            titleText.color = unequippedColor;
        damageText.text = "Damage: " + weaponDetails.damage;
        cooldownText.text = "Cooldown: " + weaponDetails.cooldown;

        if (purchased)
        {
            //costText.text = "";
            weaponImage.sprite = weaponDetails.uiImage;
        }
        else
        {
            //costText.text = "Cost: " + weaponDetails.cost;
            weaponImage.sprite = lockSprite;
        }
    }
}
