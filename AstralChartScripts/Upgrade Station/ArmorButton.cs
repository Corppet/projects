using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Selectable armor in the shop.
/// </summary>
[RequireComponent(typeof(Button))]
public class ArmorButton : UpgradeButton
{
    // Other Serialized Fields
    [SerializeField] protected TMP_Text defenseText;

    /// <summary>
    /// Update information displayed on the button.
    /// </summary>
    public void updateDetails()
    {
        ArmorUpgrade armorDetails = (ArmorUpgrade)upgradeDetails;

        titleText.text = armorDetails.upgradeName;
        defenseText.text = "Defense: +" + armorDetails.defense;

        if (purchased)
            costText.text = "";
        else
            costText.text = "Cost: " + armorDetails.cost;
    }
}
