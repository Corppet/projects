using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStationShip : MonoBehaviour
{
    // Adjustable Values
    public List<WeaponUpgrade> startingWeapons;
    //public List<ArmorUpgrade> startingArmor;
    public MainframeUpgrade startingMainframe;
    //public List<CrewUpgrade> startingCrewmates;

    [Space]

    // Other Serialized Fields
    [SerializeField] private ShopManager shopManager;
    //[SerializeField] private Transform weaponCrewSlot;
    [SerializeField] private WeaponImageReader weaponImageReader;
    [SerializeField] private Sprite noWeaponSprite;

    // Private Variables
    private List<Transform> weaponPoints;
    //private List<Transform> armorPoints;
    private List<WeaponUpgrade> weaponSlots;
    //private List<ArmorUpgrade> armorSlots;
    private MainframeUpgrade mainframe;
    //private List<CrewUpgrade> crewmates;

    private GameObject player;

    public void setWeaponSlot(int slot, WeaponUpgrade weaponDetails)
    {
        removeWeapon(slot);

        weaponSlots[slot] = weaponDetails;

        ShipWeapon weapon = Instantiate(weaponDetails.upgradePrefab, weaponPoints[slot]).GetComponent<ShipWeapon>();

        weapon.SetSlot(slot);
        weapon.SetRoot(player.transform);
        weapon.SetPlayer();
    }

    public void removeWeapon(int slot)
    {
        if (weaponSlots[slot] != null)
            for (int i = weaponPoints[slot].childCount - 1; i >= 0; i--)
                Destroy(weaponPoints[slot].GetChild(i).gameObject);

        weaponImageReader.SetImage(noWeaponSprite, slot);
    }

    public bool removeWeapon(WeaponUpgrade weapon)
    {
        int slotIndex = 0;
        for (; slotIndex < weaponSlots.Count; slotIndex++)
            if (weaponSlots[slotIndex].Equals(weapon))
            {
                removeWeapon(slotIndex);
                return true;
            }

        return false;
    }

    /*
    public void setArmorSlot(int slot, ArmorUpgrade armorDetails)
    {
        armorSlots[slot] = armorDetails;

        if (armorSlots[slot])
            Destroy(armorPoints[slot].GetChild(0).gameObject);
        Instantiate(armorDetails.upgradePrefab, armorPoints[slot]);
    }
    */

    public void setMainframe(MainframeUpgrade mainframeDetails)
    {
        int i = 0; // list iterator

        mainframe = mainframeDetails;
        for (i = gameObject.transform.childCount - 1; i >= 0; i--)
            Destroy(gameObject.transform.GetChild(i).gameObject);
        GameObject prefab = Instantiate(mainframe.upgradePrefab, gameObject.transform);
        player = prefab;

        // Find the weapon and armor points from the serialized prefab
        Transform point = prefab.transform.GetChild(0).Find("Weapon Points");
        if (point)
            for (i = 0; i < point.childCount; i++)
                weaponPoints.Add(point.GetChild(i));

        //point = prefab.transform.Find("Armor Points");
        //if (point)
        //    for (i = 0; i < point.childCount; i++)
        //        armorPoints.Add(point.GetChild(i));

        if (weaponPoints.Count >= weaponSlots.Count)
        {
            for (i = 0; i < weaponSlots.Count; i++)
                setWeaponSlot(i, weaponSlots[i]);
            for (; i < weaponPoints.Count; i++)
                weaponSlots.Add(null);
        }
        else
        {
            for (i = 0; i < weaponSlots.Count; i++)
                setWeaponSlot(i, weaponSlots[i]);
            weaponSlots.RemoveRange(weaponPoints.Count,
                weaponSlots.Count - weaponPoints.Count);
        }

        //if (armorPoints.Count > armorSlots.Count)
        //{
        //    for (i = 0; i < armorSlots.Count; i++)
        //        setArmorSlot(i, armorSlots[i]);
        //    for (; i < armorPoints.Count; i++)
        //        armorSlots.Add(null);
        //}
        //else
        //{
        //    for (i = 0; i < armorSlots.Count; i++)
        //        setArmorSlot(i, armorSlots[i]);
        //    armorSlots.RemoveRange(armorPoints.Count,
        //        armorSlots.Count - armorPoints.Count);
        //}
    }
    
    /*
    public void addCrewmate(CrewUpgrade crewmate)
    {
        crewmates.Add(crewmate);
    }
    */

    public List<WeaponUpgrade> getWeaponSlots() { return weaponSlots; }
    //public List<ArmorUpgrade> getArmorSlots() { return armorSlots; }
    //public List<CrewUpgrade> getCrewmates() { return crewmates; }

    public List<Transform> getWeaponPoints() { return weaponPoints; }

    public static UpgradeStationShip Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
		else
            Destroy(gameObject);

        weaponPoints = new List<Transform>();
        //armorPoints = new List<Transform>();

        // Configure starting ship
        weaponSlots = new List<WeaponUpgrade>(startingWeapons);
        //armorSlots = new List<ArmorUpgrade>(startingArmor);
        setMainframe(startingMainframe);

        // Configure crewmates
        //crewmates = startingCrewmates;
    }
}
