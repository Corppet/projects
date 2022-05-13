using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    [SerializeField] UpgradeStationShip uss;
    [SerializeField] GameObject missileIndicator;

    private List<GameObject> weapons;
    private List<GameObject> currentPlayerWeapons;
    private bool closed;

    private Animation wAnimation;
    private UIManager uiMan;

    // Start is called before the first frame update
    void Start(){
        uiMan = transform.parent.GetComponent<UIManager>();
        wAnimation = this.transform.GetChild(0).GetComponent<Animation>();
        //weapons = new List<GameObject>();
        //AddWeaponSlots();
        //CheckForWeapons(uss.getWeaponSlots());
    }

    // UI Management
    public bool isClosed(){ return closed; }

    public void ButtonPressed(){
        if(closed)
            Open();
        else
            Close();
    }

    public void Close(){
        uiMan.reopenUI(UIManager.UIType.WeaponUI);
        closed = true;
        wAnimation.Play("WeaponUICloseAnim");
    }

    public void Open(){
        uiMan.openUI(UIManager.UIType.WeaponUI);
        closed = false;
        wAnimation.Play("WeaponUIOpenAnim");
    }

    public void MatchClose(bool match){
        closed = match;
        ButtonPressed();
    }

    /* DEPRECATED

    // Weapons
    void FindPlayerWeapon(){
        currentPlayerWeapons = new List<GameObject>();
        GameObject wp = GameObject.Find("Weapon Points");
        foreach(Transform child in wp.transform){
            if(child.childCount > 0)
                currentPlayerWeapons.Add(child.GetChild(0).gameObject);
        }
    }

    void AddWeaponSlots(){
        Transform child = transform.GetChild(0);
        foreach(Transform w in child){
            weapons.Add(w.gameObject);
        }
    }

    void CheckForWeapons(List<WeaponUpgrade> newWeapons){
        FindPlayerWeapon();
        int count = 0;
        foreach(WeaponUpgrade wu in newWeapons){
            if(wu != null){
                weapons[count].transform.GetChild(0).GetComponent<Image>().sprite = wu.uiImage;
                if(wu.weaponType == WeaponType.Active) 
                    AddSkillShot(wu.upgradeName, currentPlayerWeapons[count]);
            }
            count++;
        }
        foreach(GameObject w in weapons){
            if(w.transform.GetChild(0).GetComponent<Image>().sprite == null)
                w.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void AddSkillShot(string nameType, GameObject weap){
        if(nameType == "Missile Launcher"){
            GameObject mlUI = Instantiate(missileIndicator, this.transform.position, Quaternion.identity);
            mlUI.GetComponent<MissileLauncherWeaponUI>().SetMissilePods(weap.GetComponent<MissilePodController>());
        }
    }

    */
}
