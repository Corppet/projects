using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

/// <summary>
/// Class to store each upgrade and their status in the shop.
/// </summary>
public class Option
{
    public Option(UpgradeObject up, GameObject bt = null, bool pr = false, bool eq = false)
    {
        upgrade = up;
        button = bt;
        purchased = pr;
        equipped = eq;
    }

    public UpgradeObject upgrade;
    public GameObject button;
    public bool purchased;
    public bool equipped;
}

/// <summary>
/// Compare class for Option. Used for sorting by name and purchase status.
/// </summary>
public class UpgradeComparer : IComparer<Option>
{
    public int Compare(Option left, Option right)
    {
        if (left.upgrade == null)
        {
            if (right.upgrade == null)
                return 0;
            else
                return -1;
        }
        else
        {
            if (right.upgrade == null)
                return 1;
            else
            {
                if (left.purchased)
                {
                    if (right.purchased)
                        return left.upgrade.upgradeName.CompareTo(right.upgrade.upgradeName);
                    else
                        return -1;
                }
                else
                {
                    if (right.purchased)
                        return 1;
                    else
                        return left.upgrade.upgradeName.CompareTo(right.upgrade.upgradeName);
                }
            }
        }
    }
}

/// <summary>
/// Core manager for the shop.
/// </summary>
public class ShopManager : MonoBehaviour
{
    // Adjustable Values
    [SerializeField] KeyCode toggleKey;
    [HideInInspector] public int coins = 0; // player's currency
    public List<UpgradeObject> upgrades; // all available upgrade items
    //[HideInInspector] public ShopTrigger trigger;
    [HideInInspector] public List<Option> weaponUpgrades;
    //[HideInInspector] public List<Option> armorUpgrades;
    //[HideInInspector] public List<Option> crewmateUpgrades;

    [Space]

    // Serialized Fields
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject weaponPrefab;
    //[SerializeField] private GameObject armorPrefab;
    //[SerializeField] private GameObject crewmatePrefab;
    [SerializeField] private TMP_Dropdown slotDropdown;
    //[SerializeField] private TMP_Dropdown typeDropdown;
    //[SerializeField] private Button purchaseButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unequipButton;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject content;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text requirementText;
    //[SerializeField] private TMP_Text coinsText;
    [SerializeField] private GameObject previewObject;
    [SerializeField] private VideoPlayer previewPlayer;
    [SerializeField] private Animation shopAnim;
    //[SerializeField] private GameObject idleObject; // for instantiating crew cards
    public GameObject openButton;

    [Space]

    [SerializeField] private AudioSource SoundPlay;
    [SerializeField] private AudioClip Purchasing;
    [SerializeField] private AudioClip EquipWeapon;
    [SerializeField] private AudioClip EquipArmor;
    [SerializeField] private AudioClip Unable;

    // Private Variables
    private GameObject selectedUpgrade;
    private UpgradeType slotType;
    private bool closed;
    private bool tutorialOn;

    /*
    public void setCoins(int value)
    {
        coins = value;
        coinsText.text = value.ToString();
    }
    */

    /// <summary>
    /// Set the currently selected upgrade.
    /// </summary>
    /// <param name="upButton">
    /// Currently selected GameObject.
    /// </param>
    public void setSelected(GameObject upButton)
    {
        selectedUpgrade = upButton;
        UpgradeButton button = upButton.GetComponent<UpgradeButton>();
        descriptionText.text = button.upgradeDetails.description;

        if (button.upgradeDetails.upgradeType == UpgradeType.Weapon)
        {
            if (((WeaponUpgrade)button.upgradeDetails).previewClip != null)
            {
                previewPlayer.clip = ((WeaponUpgrade)button.upgradeDetails).previewClip;
                previewObject.SetActive(true);
                previewPlayer.Play();
            }
            else
            {
                previewObject.SetActive(false);
                previewPlayer.Stop();
            }

            if (weaponUpgrades[button.optionIndex].purchased)
            {
                //purchaseButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false);
                unequipButton.gameObject.SetActive(false);

                if (weaponUpgrades[button.optionIndex].equipped)
                {
                    unequipButton.gameObject.SetActive(true);
                    unequipButton.interactable = true;
                }
                else
                {
                    equipButton.gameObject.SetActive(true);
                    equipButton.interactable = true;
                }

                requirementText.text = "";
            }
            else
            {
                //purchaseButton.gameObject.SetActive(true);
                //if (coins >= button.upgradeDetails.cost)
                //    purchaseButton.interactable = true;
                //else
                //    purchaseButton.interactable = false;
                //equipButton.gameObject.SetActive(false);
                //unequipButton.gameObject.SetActive(false);

                equipButton.gameObject.SetActive(true);
                equipButton.interactable = false;
                unequipButton.gameObject.SetActive(false);

                requirementText.text = "Complete " + ((WeaponUpgrade)button.upgradeDetails).bounty.bountyName 
                    + " to unlock.";
            }
        }
        //else if (button.upgradeDetails.upgradeType == UpgradeType.Armor)
        //{
        //    previewObject.SetActive(false);
        //    previewPlayer.Stop();

        //    if (armorUpgrades[button.optionIndex].purchased)
        //    {
        //        purchaseButton.gameObject.SetActive(false);
        //        equipButton.gameObject.SetActive(true);

        //        if (armorUpgrades[button.optionIndex].equipped)
        //            equipButton.interactable = false;
        //        else
        //            equipButton.interactable = true;
        //    }
        //    else
        //    {
        //        purchaseButton.gameObject.SetActive(true);
        //        if (coins >= button.upgradeDetails.cost)
        //            purchaseButton.interactable = true;
        //        else
        //            purchaseButton.interactable = false;
        //        equipButton.gameObject.SetActive(false);
        //    }
        //}
        //else
        //{
        //    previewObject.SetActive(false);
        //    previewPlayer.Stop();

        //    purchaseButton.gameObject.SetActive(true);
        //    if (!crewmateUpgrades[button.optionIndex].purchased && coins >= button.upgradeDetails.cost)
        //        purchaseButton.interactable = true;
        //    else
        //        purchaseButton.interactable = false;
        //    equipButton.gameObject.SetActive(false);
        //}
    }

    /*
    /// <summary>
    /// Purchase the selected upgrade.
    /// </summary>
    public void purchase()
    {
        UpgradeButton up = selectedUpgrade.GetComponent<UpgradeButton>();

        if (up && coins >= up.upgradeDetails.cost)
        {
            setCoins(coins - up.upgradeDetails.cost);
            SoundPlay.clip = Purchasing;
            SoundPlay.Play();

            // Mark the upgrade as purchased and re-sort the list
            if (up.upgradeDetails.upgradeType == UpgradeType.Weapon)
            {
                weaponUpgrades[up.optionIndex].purchased = true;

                up.purchased = true;
                ((WeaponButton)up).updateDetails();
            }
            //else if (up.upgradeDetails.upgradeType == UpgradeType.Armor)
            //{
            //    armorUpgrades[up.optionIndex].purchased = true;

            //    up.purchased = true;
            //    ((ArmorButton)up).updateDetails();
            //}
            //else if (up.upgradeDetails.upgradeType == UpgradeType.Crewmate)
            //{
            //    crewmateUpgrades[up.optionIndex].purchased = true;
            //    crewmateUpgrades[up.optionIndex].equipped = true;
            //    Instantiate(crewmateUpgrades[up.optionIndex].upgrade.upgradePrefab, idleObject.transform);

            //    up.purchased = true;
            //    ((CrewButton)up).updateDetails();
            //}
        }
        else
        {
            SoundPlay.clip = Unable;
            SoundPlay.Play();
        }

        setSelected(selectedUpgrade);
    }
    */

    /// <summary>
    /// Equip the selected upgrade.
    /// </summary>
    public void equip()
    {
        UpgradeStationShip shipScript = playerShip.GetComponent<UpgradeStationShip>();

        if (slotType == UpgradeType.Weapon)
        {
            WeaponButton button = selectedUpgrade.GetComponent<WeaponButton>();
            WeaponUpgrade prevWeapon = shipScript.getWeaponSlots()[slotDropdown.value];
            if (prevWeapon != null)
                foreach (Option i in weaponUpgrades)
                    if (i.upgrade.Equals(prevWeapon))
                    {
                        i.equipped = false;
                        break;
                    }
            shipScript.setWeaponSlot(slotDropdown.value, (WeaponUpgrade)button.upgradeDetails);
            weaponUpgrades[button.optionIndex].equipped = true;
            button.equipped = true;
            button.updateDetails();

            SoundPlay.clip = EquipWeapon;
            SoundPlay.Play();
        }
        //else if (slotType == UpgradeType.Armor)
        //{
        //    ArmorButton button = selectedUpgrade.GetComponent<ArmorButton>();

        //    shipScript.setArmorSlot(slotDropdown.value, (ArmorUpgrade)button.upgradeDetails);
        //    armorUpgrades[button.optionIndex].equipped = true;

        //    SoundPlay.clip = EquipArmor;
        //    SoundPlay.Play();
        //}

        setSelected(selectedUpgrade);
    }

    public void unequip()
    {
        UpgradeStationShip shipScript = playerShip.GetComponent<UpgradeStationShip>();

        if (slotType == UpgradeType.Weapon)
        {
            WeaponButton button = selectedUpgrade.GetComponent<WeaponButton>();

            shipScript.removeWeapon((WeaponUpgrade)button.upgradeDetails);
            weaponUpgrades[button.optionIndex].equipped = false;
            button.equipped = false;
            button.updateDetails();
        }

        setSelected(selectedUpgrade);
    }

    /// <summary>
    /// Update what the shop shows according to the selected slot type.
    /// </summary>
    public void updateShop()
    {
        GameObject upButtonInstance;
        UpgradeComparer comparer = new UpgradeComparer();

        // Delete any existing options
        for (int i = content.transform.childCount - 1; i >= 0; i--)
            Destroy(content.transform.GetChild(i).gameObject);

        if (slotType == UpgradeType.Weapon)
        {
            //weaponUpgrades.Sort(comparer);

            // Create a new button for each upgrade
            for (int i = 0; i < weaponUpgrades.Count; i++)
            {
                upButtonInstance = Instantiate(weaponPrefab, content.transform);

                WeaponButton upButtonScript = upButtonInstance.GetComponent<WeaponButton>();
                upButtonScript.shopManager = gameObject.GetComponent<ShopManager>();
                upButtonScript.upgradeDetails = weaponUpgrades[i].upgrade;
                upButtonScript.optionIndex = i;
                upButtonScript.purchased = weaponUpgrades[i].purchased;
                upButtonScript.equipped = weaponUpgrades[i].equipped;
                upButtonScript.updateDetails();

                weaponUpgrades[i].button = upButtonInstance;
            }
        }
        //else if (slotType == UpgradeType.Armor)
        //{
        //    armorUpgrades.Sort(comparer);

        //    for (int i = 0; i < armorUpgrades.Count; i++)
        //    {
        //        upButtonInstance = Instantiate(armorPrefab, content.transform);

        //        ArmorButton upButtonScript = upButtonInstance.GetComponent<ArmorButton>();
        //        upButtonScript.shopManager = gameObject.GetComponent<ShopManager>();
        //        upButtonScript.upgradeDetails = armorUpgrades[i].upgrade;
        //        upButtonScript.optionIndex = i;
        //        upButtonScript.updateDetails();

        //        armorUpgrades[i].button = upButtonInstance;
        //    }
        //}
        //else if (slotType == UpgradeType.Crewmate)
        //{
        //    crewmateUpgrades.Sort(comparer);

        //    for (int i = 0; i < crewmateUpgrades.Count; i++)
        //    {
        //        if (!crewmateUpgrades[i].purchased)
        //        {
        //            upButtonInstance = Instantiate(crewmatePrefab, content.transform);

        //            CrewButton upButtonScript = upButtonInstance.GetComponent<CrewButton>();
        //            upButtonScript.shopManager = gameObject.GetComponent<ShopManager>();
        //            upButtonScript.upgradeDetails = crewmateUpgrades[i].upgrade;
        //            upButtonScript.optionIndex = i;
        //            upButtonScript.updateDetails();

        //            crewmateUpgrades[i].button = upButtonInstance;
        //        }
        //    }
        //}

        //setCoins(coins);
    }

    /// <summary>
    /// Add upgrade slots if applicable to the current type.
    /// </summary>
    public void updateSlots()
    {
        UpgradeStationShip shipScript = playerShip.GetComponent<UpgradeStationShip>();
        slotDropdown.ClearOptions();

        if (slotType == UpgradeType.Weapon)
        {
            for (int i = 0; i < shipScript.getWeaponSlots().Count; i++)
                slotDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Weapon " + (i + 1) });
            slotDropdown.enabled = true;
        }
        else
            slotDropdown.enabled = false;

        slotDropdown.RefreshShownValue();
    }

    /*
    /// <summary>
    /// Change the type of upgrades to view by upgrade type.
    /// </summary>
    public void setSlot()
    {
        if (typeDropdown.value == 0)
            slotType = UpgradeType.Weapon;
        else if (typeDropdown.value == 1)
            slotType = UpgradeType.Armor;
        else
            slotType = UpgradeType.Crewmate;

        updateSlots();
        updateShop();
    }
    */

    /// <summary>
    /// Distribute the list of upgrades into seperate lists by upgrade type.
    /// </summary>
    private void setupLists()
    {
        weaponUpgrades = new List<Option>();
        //armorUpgrades = new List<Option>();
        //crewmateUpgrades = new List<Option>();

        UpgradeStationShip uss = playerShip.GetComponent<UpgradeStationShip>();
        foreach (WeaponUpgrade i in uss.startingWeapons)
            weaponUpgrades.Add(new Option(i, null, true, true));
        //foreach (ArmorUpgrade i in uss.startingArmor)
        //    armorUpgrades.Add(new Option(i, null, true, true));
        //foreach (CrewUpgrade i in uss.startingCrewmates)
        //    crewmateUpgrades.Add(new Option(i, null, true, true));

        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].upgradeType == UpgradeType.Weapon)
                weaponUpgrades.Add(new Option(upgrades[i]));
            //else if (upgrades[i].upgradeType == UpgradeType.Armor)
            //    armorUpgrades.Add(new Option(upgrades[i]));
            //else if (upgrades[i].upgradeType == UpgradeType.Crewmate)
            //    crewmateUpgrades.Add(new Option(upgrades[i]));
        }

        if (weaponUpgrades.Count != 0)
            slotType = UpgradeType.Weapon;
        //else if (armorUpgrades.Count != 0)
        //    slotType = UpgradeType.Armor;
        //else
        //    slotType = UpgradeType.Crewmate;

        updateShop();
        updateSlots();
    }

    /// <summary>
    /// Checks if the shop UI is closed.
    /// </summary>
    /// <returns></returns>
    public bool isClosed() { return closed; }

    /// <summary>
    /// Open the shop UI.
    /// </summary>
    public void Open()
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.OpenShop);
        shopAnim.Play("ShopOpen");
        closed = false;
        uiManager.openUI(UIManager.UIType.UpgradeStation);
        playerShip.GetComponentInChildren<ShipController>().movementEnabled = false;
        playerShip.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        openButton.SetActive(false);
    }

    /// <summary>
    /// Close the shop UI.
    /// </summary>
    public void Close()
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.CloseShop);
        shopAnim.Play("ShopClose");
        closed = true;
        uiManager.reopenUI(UIManager.UIType.UpgradeStation);
        playerShip.GetComponentInChildren<ShipController>().movementEnabled = true;
        openButton.SetActive(true);
    }

    /*
    /// <summary>
    /// Used for the close button in the shop.
    /// </summary>
    public void CloseButton()
    {
        if (trigger)
            trigger.CloseShop();
        else if (!isClosed()) // safety net
            Close();
    }
    */

    private void Start()
    {
        shopAnim.transform.localScale = new Vector3(1f, 0f, 1f);
        descriptionText.text = "";
        //purchaseButton.interactable = false;
        equipButton.interactable = false;
        previewObject.SetActive(false);
        previewPlayer.Stop();
        requirementText.text = "";
        closed = true;
        setupLists();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (closed)
                Open();
            else
                Close();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !closed)
            Close();
    }
}