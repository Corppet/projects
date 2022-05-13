using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

	GameObject placeholder = null;

	[SerializeField] DialogueCharacter character;
	[SerializeField] TMP_Text name;
	[SerializeField] Image picture;

	private bool piloting;
    private bool sharpshooter;
    private bool engineering;
	private string details;

	[Space]

	// Stamina
	[SerializeField] Slider staminaBar;
	private float stamina;
	private float maxStamina = 100f;
	[SerializeField] float setStamina = 100.0f;

	[Space]

	[SerializeField] bool movable = true;

	private Coroutine hoverCoroutine;
	private CardReader cr;
	private bool onHover;

	void Awake(){
		stamina = setStamina;
		staminaBar.maxValue = maxStamina;
		staminaBar.value = stamina;
		cr = GameObject.Find("ExamineCardArea").GetComponent<CardReader>();

		// read the character and place it in the card
		this.piloting = character.piloting;
		this.sharpshooter = character.sharpshooter;
		this.engineering = character.engineering;
		this.details = character.details;
		picture.sprite = character.portrait; 
		name.text = character.short_name;
		onHover = false;
	}

	public void makeMovable()					{ movable = true; }

	public bool getPiloting()					{ return piloting; }
	public bool getSharpshooting()				{ return sharpshooter; }
	public bool getEngineering()				{ return engineering; }
	public string getName()						{ return character.short_name; }
	public string getDetails()					{ return character.details; }
	public DialogueCharacter getCharacter()		{ return character; }
	public Sprite getImage()					{ return picture.sprite; }

	public bool hasStamina(){ return (stamina > 0.0f); }
	public float getStamina(){ return stamina; }

	public void upgradePiloting()		{ piloting = true;		}
	public void upgradeSharpShooting()	{ sharpshooter = true;	}
	public void upgradeEngineering()	{ engineering = true;	}

	public void ConsumeStamina(float value){ stamina -= value; staminaBar.value = stamina; }
	public void RestoreStamina(float value){ stamina = Mathf.Min(stamina + value, maxStamina); staminaBar.value = stamina; }

	// On hover
	public void OnPointerEnter(PointerEventData eventData){
		hoverCoroutine = StartCoroutine(Hover());
    }

	// Off hover
    public void OnPointerExit(PointerEventData eventData){
		if(hoverCoroutine != null)
			StopAllCoroutines();
		// zoom back
		if(onHover){
			cr.CloseCard();
			onHover = false;
		}
    }

	IEnumerator Hover(){
		yield return new WaitForSeconds(1.2f);
		onHover = true;
		cr.EmptyValues();
		cr.SetValues(piloting, sharpshooter, engineering, details, character.name, character.portrait);
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		if(!movable) return;
		placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent(this.transform.parent.parent);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	public void OnDrag(PointerEventData eventData) {
		if(!movable) return;
		this.transform.position = eventData.position;

		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);

		int newSiblingIndex = placeholderParent.childCount;
		for(int i = 0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {
				newSiblingIndex = i;
				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;
				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		if(!movable) return;
		this.transform.SetParent( parentToReturnTo );
		this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		Destroy(placeholder);
	}
}
