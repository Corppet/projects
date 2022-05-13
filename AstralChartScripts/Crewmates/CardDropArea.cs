using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class CardDropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public enum Spot {
		PILOT, ENGINEER, QUARTERS, IDLE, WEAPON
	}

	[SerializeField] int totalAbleToDrop = 1;
	[SerializeField] Spot designatedSpot;

	[Space]

	private DialogueCrewmates dcsys;
	private GameObject dialogueEnable1;
	private GameObject dialogueEnable2;
	private GameObject playerShip;
	private TankControlsLockOn controls;

	[Space]

	public AudioSource soundplay;
	public AudioClip audiosound;
	//public GameObject AudioController;

	// flags
	private bool cannotDrop;
	private bool repairing;

	protected float timer;

	void Start(){
		repairing = false;
		cannotDrop = false;
		if(designatedSpot == Spot.PILOT){
			playerShip = GameObject.Find("PlayerHolder").transform.GetChild(0).gameObject;
			controls = playerShip.GetComponent<TankControlsLockOn>();
		} else if(designatedSpot == Spot.ENGINEER){
			playerShip = GameObject.Find("PlayerHolder").transform.GetChild(0).gameObject;
		}
	}

	void Update(){
		timer += Time.deltaTime;
		// called every half second
		if(timer >= 0.5f){
			switch(designatedSpot){
				case Spot.ENGINEER:
					Repair();
					break;
				case Spot.PILOT:
					//Pilot();
					break;
				case Spot.IDLE:
					Idle();
					break;
			}
			timer = 0.0f;
		}
	}

	void CardAvailability(){
		cannotDrop = this.transform.childCount > totalAbleToDrop ? true : false;
	}
	
	void AddCard(Card card){
		// Card is added (ADD SOUND HERE)
		switch(designatedSpot){
			case Spot.ENGINEER:
				// PLAY SOUND WHEN CARD ENTERS THE ENGINEERING BAY

				SoundPlayer();
				break;
			case Spot.PILOT:
				Pilot(card);
				// PLAY SOUND WHEN CARD ENTERS THE PILOTING SEAT
				SoundPlayer();
				break;
			case Spot.WEAPON:
				// PLAY SOUND WHEN CARD ENTERS THE WEAPON SPOT
				Weapon(card);
				SoundPlayer();
				break;
			case Spot.IDLE:
				SoundPlayer();
				break;
		}
	}

	void SoundPlayer()
    {
		soundplay.Stop();
		soundplay.clip = audiosound;
		soundplay.Play();
    }

	void RemoveCard(Card card){
		switch(designatedSpot){
			case Spot.ENGINEER:
				Repair();
				break;
			case Spot.PILOT:
				Pilot(card);
				break;
			case Spot.IDLE:
				Idle();
				break;
			case Spot.WEAPON:
				// PLAY SOUND WHEN CARD ENTERS THE WEAPON SPOT
				Weapon(card);
				break;
		}
	}

	public void Weapon(Card card){
		if(this.transform.childCount == 1 && card != null){
			if(card.getSharpshooting()){
				Actions.ActivateWeapon(true);
				CrewmateEvent.activeWeapon.AddListener(ActiveWeapon);
			} else {
				Actions.ActivateWeapon(false);
			}
		} else {
			Actions.ActivateWeapon(false);
			CrewmateEvent.activeWeapon.RemoveListener(ActiveWeapon);
		}
	}

	public void ActiveWeapon(){
		if(designatedSpot != Spot.WEAPON || this.transform.childCount == 0)
			return;
		Card d = this.transform.GetChild(0).GetComponent<Card>();
		if(this.transform.childCount == 1 && d != null && d.hasStamina()){
			d.ConsumeStamina(1.0f);
		}
	}

	void Idle(){
		foreach(Transform child in transform){
			Card d = null;
			if((d = child.GetComponent<Card>()) != null){
				d.RestoreStamina(5.0f);
			}
		}
	}

	void Pilot(Card card){
		if(this.transform.childCount == 1 && card != null){
			if(card.getPiloting()){
				CrewmateEvent.activePiloting.AddListener(ActivePiloting);
				controls.Activate(true);
			}
		} else {
			CrewmateEvent.activePiloting.RemoveListener(ActivePiloting);
			controls.Activate(false);
		}
	}

	public void ActivePiloting(){
		if(designatedSpot != Spot.PILOT || this.transform.childCount == 0)
			return;
		Card d = this.transform.GetChild(0).GetComponent<Card>();
		if(this.transform.childCount == 1 && d != null && d.hasStamina()){
			d.ConsumeStamina(0.1f);
		} else {
			controls.Activate(false);
		}
	}

	void Repair(){
		Card p2 = null, p1 = null;
		if(this.transform.childCount == 1)
			p1 = this.transform.GetChild(0).GetComponent<Card>();
		if(this.transform.childCount == 2)
			p2 = this.transform.GetChild(1).GetComponent<Card>();
		float temp = 0.0f;
		if(p1 != null){
			p1.ConsumeStamina(0.1f);
			temp += 1.0f;
		}
		if(p2 != null){
			p2.ConsumeStamina(0.1f);
			temp += 1.0f;
		}
		
		if(temp > 0){
			if(!repairing)
				repairing = true;
		} else {
			repairing = false;
		}
		//repairing = (temp > 0.0f) ? true : false;
		if(repairing) playerShip.GetComponent<ShipHealth>().AddHealth(temp);
	}

	public void OnPointerEnter(PointerEventData eventData) {
		CardAvailability();
		if(eventData.pointerDrag == null || cannotDrop)
			return;
        
		Card d = eventData.pointerDrag.GetComponent<Card>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		CardAvailability();
		if(eventData.pointerDrag == null || cannotDrop)
			return;

		Card d = eventData.pointerDrag.GetComponent<Card>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
		RemoveCard(d);
	}
	
	public void OnDrop(PointerEventData eventData) {
		CardAvailability();
		if(cannotDrop) return;
		Card d = eventData.pointerDrag.GetComponent<Card>();
		if(d != null) {
			d.parentToReturnTo = this.transform;
			AddCard(d);
		}
	}
}
