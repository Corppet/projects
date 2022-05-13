using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Selectable upgrade in the shop.
/// </summary>
[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    // Adjustable Values
    [HideInInspector] public UpgradeObject upgradeDetails;
    [HideInInspector] public ShopManager shopManager;
    [HideInInspector] public bool purchased;
    [HideInInspector] public bool equipped;
    [HideInInspector] public int optionIndex;

    // Other Serialized Fields
    [SerializeField] protected TMP_Text titleText;
    [SerializeField] protected TMP_Text costText;

    /// <summary>
    /// Set the currently selected upgrade.
    /// </summary>
    public void selectUpgrade()
    {
        shopManager.setSelected(gameObject);
    }
}
