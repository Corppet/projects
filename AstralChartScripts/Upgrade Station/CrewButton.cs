using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Selectable crewmate in the shop.
/// </summary>
[RequireComponent(typeof(Button))]
public class CrewButton : UpgradeButton
{
    // Other Serialized Fields
    [SerializeField] Image pilotIcon;
    [SerializeField] Image weaponIcon;
    [SerializeField] Image engineerIcon;

    public void updateDetails()
    {
        CrewUpgrade crewmateDetails = (CrewUpgrade)upgradeDetails;

        titleText.text = crewmateDetails.upgradeName;

        if (!crewmateDetails.piloting)
            pilotIcon.color = Color.clear;
        if (!crewmateDetails.weaponry)
            weaponIcon.color = Color.clear;
        if (!crewmateDetails.engineering)
            engineerIcon.color = Color.clear;

        if (purchased)
            costText.text = "";
        else
            costText.text = "Cost: " + crewmateDetails.cost;
    }
}