using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public enum TutorialEvent { 
    None, OnGameStarted, KeyPress, Scroll, Minimap, 
    EnterCombat, ExitCombat, Weapons, ActivatingWeapons,
    OpenBountyBoard, UsingBountyBoard, InBounty, CloseBountyBoard, 
    Pursue, Claim, OpenShop, CloseShop
};

/*
None, OnGameStarted, Movement, Scroll, Minimap, 
EnterCombat, InCombat, Weapons, ActivatingWeapons,
OpenBountyBoard, UsingBountyBoard, InBounty, End
*/
[System.Serializable]
public class TutorialPrompt
{
    public Sprite icon;
    public Sprite keyIcon;
    public string text;
    public TutorialEvent activationEvent;
    public float duration = 3.0f;
    public UnityEvent OnPromptDisappear = new UnityEvent();
    public List<KeyCode> key;
    public int listIndex;
    public GameObject activeObject;
}

public class TutorialManager : MonoBehaviour
{
    [Tooltip("Used for testing. Should be true in final build.")]
    [SerializeField] private bool enableTutorial = true;

    [Space]

    public static TutorialManager instance;
    public Image iconImage;
    public Image finishedImage;
    public Image keyImage;

    public TMPro.TextMeshProUGUI Text;

    public List<TutorialPrompt> TutorialPrompts = new List<TutorialPrompt>();


    private Dictionary<TutorialEvent, TutorialPrompt> TutorialPrompts_Dict = new Dictionary<TutorialEvent, TutorialPrompt>();
    private AnimatedPanel panel;
    public TutorialPrompt activePrompt;
    public TutorialPrompt activeEvent;

    //private UnityEvent FailureOnPromptDisappear = new UnityEvent();
    private TankControlsLockOn tclo;
    private BountyBoardManager bbm;

    // Flags
    private bool reference;
    private bool inCombat;
    private bool combatCompleted;
    private bool returnCoroutine;
    private bool endTutorial;

    public Sprite checkSprite;
    public Sprite xSprite;

    private void Awake(){
        instance = this;
        endTutorial = false;
        returnCoroutine = false;
        panel = GetComponent<AnimatedPanel>();
        tclo = (TankControlsLockOn) FindObjectOfType(typeof(TankControlsLockOn));
        bbm = (BountyBoardManager) FindObjectOfType(typeof(BountyBoardManager));
    }

    // Start is called before the first frame update
    void Start(){
        if(PlayerPrefs.GetInt("Tutorial") == 1) return;
        DisplayMessage(activePrompt.icon, activePrompt.keyIcon, activePrompt.text);
        InitiateTutorialPromptByIndex(0);
    }

    IEnumerator Check(){
        finishedImage.sprite = checkSprite;
        finishedImage.color = Color.green;
        yield return new WaitForSeconds(1.0f);
    }

    public void InitializeDictionary(){
        foreach(TutorialPrompt prompt in TutorialPrompts){
            if(TutorialPrompts_Dict.ContainsKey(prompt.activationEvent) == false && prompt.activationEvent != TutorialEvent.None){
                TutorialPrompts_Dict.Add(prompt.activationEvent, prompt);
            }
        }
    }

    public void OnTutorialEnd(){
        endTutorial = true;
        StopAllCoroutines();
        StartCoroutine(HideMessageEnd(activePrompt));
    }

    public void InitiateTutorialPromptByIndex(int i){
        finishedImage.sprite = xSprite;
        finishedImage.color = Color.red;
        InitiateTutorialEvent(TutorialPrompts[i].activationEvent, i);
    }

    public void InitiateTutorialEventCase(TutorialEvent Event){
        finishedImage.sprite = xSprite;
        finishedImage.color = Color.red;
        if (Event == TutorialEvent.EnterCombat) StartCoroutine(DisplayTutorialPrompt_BeginCombat(Event, activeEvent));
        DisplayMessage(activeEvent.icon, activeEvent.keyIcon,  activeEvent.text);
    }

    public void InitiateTutorialEvent(TutorialEvent Event, int i = -1){
        //if (i == -1 && Event != TutorialEvent.None) activePrompt = TutorialPrompts_Dict[Event];
        if(endTutorial) return;
        activePrompt = TutorialPrompts[i];

        if (Event == TutorialEvent.OpenBountyBoard   || Event == TutorialEvent.CloseBountyBoard || 
            Event == TutorialEvent.OpenShop          || Event == TutorialEvent.CloseShop        || 
            Event == TutorialEvent.ActivatingWeapons || Event == TutorialEvent.UsingBountyBoard) {
            StartCoroutine(DisplayTutorialPrompt_Reference(Event));
        }

        if (activePrompt.duration != -1) StartCoroutine(DisplayTutorialPrompt_Coroutine(Event, activePrompt.duration));
        else DisplayMessage(activePrompt.icon, activePrompt.keyIcon,  activePrompt.text);

        switch(Event){
            case TutorialEvent.Pursue:
                StartCoroutine(DisplayTutorialPrompt_Pursue(Event));
                break;
            case TutorialEvent.Claim:
                StartCoroutine(DisplayTutorialPrompt_Claim(Event));
                break;
            case TutorialEvent.InBounty:
                StartCoroutine(DisplayTutorialPrompt_InBounty(Event, activePrompt.activeObject));
                break;
            case TutorialEvent.KeyPress:
                StartCoroutine(DisplayTutorialPrompt_Button(Event, activePrompt.key));
                break;
            case TutorialEvent.Scroll:
                StartCoroutine(DisplayTutorialPrompt_Scroll(Event));
                break;
            case TutorialEvent.EnterCombat:
                if(activeEvent.activationEvent == TutorialEvent.None) activeEvent = TutorialPrompts[i];
                StartCoroutine(DisplayTutorialPrompt_BeginCombat(Event, activePrompt));
                break;
            case TutorialEvent.ExitCombat:
                StartCoroutine(DisplayTutorialPrompt_EndCombat(Event)); 
                activeEvent = null;
                break;
        }
    }

    // ====================================== COMBAT ======================================

    public IEnumerator DisplayTutorialPrompt_BeginCombat(TutorialEvent Event, TutorialPrompt tp){
        while(!inCombat){
            if(endTutorial) yield break;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(tp));
    }

    public void DisplayTutorialPrompt_InCombatCall(){
        if(tclo.GetTargetListCount() > 0) inCombat = true;
    }

    public void DisplayTutorialPrompt_OutCombatCall(TutorialEvent Event){
        if(tclo.GetTargetListCount() > 0) return;
        inCombat = false;
        if(activePrompt.activationEvent != Event && activeEvent != null){
            returnCoroutine = true;
            StopAllCoroutines();
            // set active event to return here (active prompt)
            activeEvent.OnPromptDisappear.RemoveAllListeners();
            activeEvent.OnPromptDisappear.AddListener(
                delegate{InitiateTutorialPromptByIndex(activePrompt.listIndex);
            });
            // set active prompt to active event
            StartCoroutine(HideMessageFailure(activeEvent.activationEvent));
        }
    }

    public IEnumerator DisplayTutorialPrompt_EndCombat(TutorialEvent Event){
        while(inCombat){
            if(endTutorial) yield break;
            if(tclo.GetTargetListCount() <= 0) inCombat = false;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }

    // ==================================== Bounty Board =====================================

    public IEnumerator DisplayTutorialPrompt_InBounty(TutorialEvent Event, GameObject obj){
        while(obj != null){
            if(endTutorial) yield break;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }

    public IEnumerator DisplayTutorialPrompt_Pursue(TutorialEvent Event){
        reference = false;
        while(!reference || !bbm.pursueStatus()){
            if(endTutorial) yield break;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }


    public IEnumerator DisplayTutorialPrompt_Claim(TutorialEvent Event){
        reference = false;
        while(!reference || !bbm.claimedStatus()){
            if(endTutorial) yield break;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }
    
    // ====================================== REFERENCE ======================================

    public IEnumerator DisplayTutorialPrompt_Reference(TutorialEvent Event){
        reference = false;
        while(!reference){
            if(endTutorial) yield break;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }

    public void DisplayTutorialPrompt_ReferenceCall(TutorialEvent Event){
        if(activePrompt.activationEvent == Event){
            reference = true;
        }
    }

    // ======================================== KEYS ========================================

    //TutorialManager.instance.InitiateTutorialEvent(TutorialEvent.);

    public IEnumerator DisplayTutorialPrompt_Scroll(TutorialEvent Event){
        while(Input.mouseScrollDelta.y == 0){
            if(endTutorial) yield break;
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }

    public IEnumerator DisplayTutorialPrompt_Button(TutorialEvent Event, List<KeyCode> key){
        bool foundKeyInput = false;
        returnCoroutine = false;
        while(true){
            if(endTutorial) yield break;
            foreach(KeyCode k in key){
                if(Input.GetKey(k)) foundKeyInput = true;
            }
            if(returnCoroutine) yield break;
            if(foundKeyInput) { break; }
            yield return null;
        }
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }

    // ===================================== MAIN ==========================================

    public IEnumerator DisplayTutorialPrompt_Coroutine(TutorialEvent Event, float dur){
        DisplayMessage(activePrompt.icon, activePrompt.keyIcon, activePrompt.text);
        yield return new WaitForSeconds(dur);
        if(endTutorial) yield break;
        yield return StartCoroutine("Check");
        StartCoroutine(HideMessage(activePrompt));
    }

    public void DisplayMessage(Sprite icon, Sprite keyIcon, string text){
        if(endTutorial) return;
        
        keyImage.sprite = keyIcon;
        iconImage.sprite = icon;
        Text.text = text;
        panel.FadeIn();
    }

    public IEnumerator HideMessage(TutorialPrompt tp){
        panel.FadeOut();
        if(endTutorial) yield break;
        yield return new WaitForSeconds(panel.timeToFade);
        tp.OnPromptDisappear.Invoke();
    }

    public IEnumerator HideMessageEnd(TutorialPrompt tp){
        panel.FadeOut();
        yield return new WaitForSeconds(panel.timeToFade);
    }

    public IEnumerator HideMessageFailure(TutorialEvent Event){
        panel.FadeOut();
        if(endTutorial) yield break;
        yield return new WaitForSeconds(panel.timeToFade);
        InitiateTutorialEventCase(Event);
    }
}
