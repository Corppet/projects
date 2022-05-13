using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class MainframeButton : UpgradeButton
{
    // Adjustable Values
    public MainframeUpgrade details;

    [Space]

    // Other Serialized Fields
    [SerializeField] protected TMP_Text ratingText;

    public void updateDetails()
    {
        titleText.text = details.upgradeName;
        ratingText.text = "Rating " + details.rating;

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
