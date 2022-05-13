using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponTreeManager : MonoBehaviour
{
    // Adjustable Values
    public int weaponPoints;

    [Space]

    // Other Serialized Fields
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private TMP_Dropdown factionDropdown;
    [SerializeField] private Button acquireButton;
    [SerializeField] private TMP_Text weaponPointsText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text weaponTypeText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text prerequisiteText;
    [SerializeField] private TMP_Text creditsCostText;
    [SerializeField] private GameObject ASTree;
    [SerializeField] private GameObject BUTree;
    [SerializeField] private GameObject FORTree;
    [SerializeField] private GameObject MRDTree;

    // Private Variables
    private WeaponTreeButton selected;
    private Faction selectedFaction;

    public void setWeaponPoints(int value)
    {
        weaponPoints = value;
        weaponPointsText.text = value.ToString();
    }

    public void setSelected(GameObject button)
    {
        selected = button.GetComponent<WeaponTreeButton>();
        WeaponUpgrade weapon = selected.details;

        if (weapon.prerequisite != null)
        {
            if (!weapon.treePurchased && weapon.prerequisite.treePurchased
                && weaponPoints >= weapon.weaponPoints)
                acquireButton.interactable = true;
            else
                acquireButton.interactable = false;

            if (!weapon.prerequisite.treePurchased)
                prerequisiteText.text = "Requires " + weapon.prerequisite.upgradeName + ".";
            else
                prerequisiteText.text = "";
        }
        else
        {
            if (!weapon.treePurchased && weaponPoints >= weapon.weaponPoints)
                acquireButton.interactable = true;
            else
                acquireButton.interactable = false;

            prerequisiteText.text = "";
        }

        titleText.text = weapon.upgradeName;
        descriptionText.text = weapon.description;
        weaponTypeText.text = "Type: " + weapon.weaponType.ToString();
        damageText.text = "Damage: " + weapon.damage;
        creditsCostText.text = "Credits: " + weapon.cost;
    }

    public void acquire()
    {
        WeaponUpgrade weapon = selected.details;

        // Play acquire sound

        weapon.treePurchased = true;
        setWeaponPoints(weaponPoints - weapon.weaponPoints);
        
        shopManager.updateShop();
        selected.updateDetails();
        acquireButton.interactable = false;
    }

    public void setFaction()
    {
        ASTree.SetActive(false);
        BUTree.SetActive(false);
        FORTree.SetActive(false);
        MRDTree.SetActive(false);

        switch (factionDropdown.value)
        {
            case 0:
                selectedFaction = Faction.AscendantSystems;
                ASTree.SetActive(true);
                break;
            case 1:
                selectedFaction = Faction.BastionUnited;
                BUTree.SetActive(true);
                break;
            case 2:
                selectedFaction = Faction.FollowingOfReunion;
                FORTree.SetActive(true);
                break;
            default:
                selectedFaction = Faction.MatrioshkaResearchDivision;
                MRDTree.SetActive(true);
                break;
        }
    }

    public void Start()
    {
        setWeaponPoints(weaponPoints);
        titleText.text = "";
        descriptionText.text = "";
        weaponTypeText.text = "";
        damageText.text = "";
        prerequisiteText.text = "";
        creditsCostText.text = "";
        acquireButton.interactable = false;
    }
}