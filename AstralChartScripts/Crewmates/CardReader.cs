using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardReader : MonoBehaviour
{

    public Image pilotSkills;
    public Image weaponSkills;
    public Image engineerSkills;

    [SerializeField] CrewButtonController cbc;

	[SerializeField] TMP_Text name;
    [SerializeField] TMP_Text details;

    [SerializeField] Image face;

    [SerializeField] Color pilotColor;
    [SerializeField] Color weaponColor;
    [SerializeField] Color engiColor;
    [SerializeField] Color blankColor;

    private bool open = false;

    public bool Opened(){ return open; }

	public void SetValues(bool pilot, bool weapon, bool engi, string det, string n, Sprite person){
        if(pilot)
            pilotSkills.color = pilotColor;
        if(weapon)
            weaponSkills.color = weaponColor;
        if(engi)
            engineerSkills.color = engiColor;
        
        details.text = det;
        name.text = n;
        face.sprite = person;

        cbc.OpenCrewCard();
        open = true;
    }

    public void CloseCard(){
        cbc.CloseCrewCard();
        open = false;
    }

    public void EmptyValues(){
        pilotSkills.color = blankColor;
        weaponSkills.color = blankColor;
        engineerSkills.color = blankColor;
        details.text = "";
        name.text = "";
    }
}
