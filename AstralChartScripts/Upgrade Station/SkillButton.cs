using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class SkillButton : UpgradeButton
{
    // Adjustable Values
    public SkillUpgrade details;

    [Space]

    // Other Serialized Fields
    [SerializeField] protected TMP_Text ratingText;
    [SerializeField] protected TMP_Text reputationText;

    public void updateDetails()
    {
        titleText.text = details.upgradeName;
        ratingText.text = "Rating: " + details.rating;
        reputationText.text = "Reputation: " + details.reputation;

        if (purchased)
            costText.text = "";
        else
            costText.text = "Cost: " + details.cost;
    }

    public void selectUpgrade()
    {
        shopManager.setSelected(gameObject);
    }
}
