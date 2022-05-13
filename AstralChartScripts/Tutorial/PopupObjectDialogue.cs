using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupObjectDialogue : PopupObject {
    // Public Variables
    /*
    [SerializeField] DialogueList dialogue;
    private DialogueInitiator dialogueManager;

    public delegate void BeginTutorialDialogue(DialogueList d);
    public static event BeginTutorialDialogue onBeginTutorialDialogue;

    private void OnEnable() {
        DialogueInitiator.onFinishTutorialDialogue += BufferNext;
    }

    private void OnDisable() {
        DialogueInitiator.onFinishTutorialDialogue -= BufferNext;
    }

    public virtual IEnumerator BufferNextDialogue()
    {
        onBeginTutorialDialogue(dialogue);
        //yield return StartCoroutine(dialogueManager.StartDialogue(dialogue));

        //tutorialManager.nextPopup();
        yield return null;
    }

    void Start()
    {
        dialogueManager = (DialogueInitiator)FindObjectOfType(typeof(DialogueInitiator));
    }

    public override void Update()
    {
        if (isOpened && !buttonPressed)
        {
            StartCoroutine(BufferNextDialogue());
            buttonPressed = true;
        }
    }

    void BufferNext(){
        tutorialManager.nextPopup();
    }*/
}