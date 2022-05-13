using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPopup : PopupObject
{
    /*
    [SerializeField] private Card carter;
    [SerializeField] private CrewBoardPopup NonSelectablePopup;
    [SerializeField] CrewButtonController cbc;
    [SerializeField] private bool SelectBack = false;

    private CrewBoardPopup ReturnPopup;
    private PointerPopup ReturnPointerPopup;

    private bool hover = false;

    public void SetReturnBoardPopup(CrewBoardPopup cbp){
        ReturnPopup = cbp;
    }

    public void ButtonPressed()
    {
        if (!buttonPressed && isOpened && !SelectBack)
        {
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !cbc.isClosed() && SelectBack)
        {
            ReturnPopup.Open();
            Close();
            hover = false;
        }
    }

    public override IEnumerator BufferNextPopup(float delay){
        for(int x = 0; x < 100; x++){
            yield return new WaitForSeconds(delay/100.0f);
            if(cbc.isClosed()){
                Close();
                NonSelectablePopup.Open();
                buttonPressed = false;
                yield break;
            }
        }
        tutorialManager.nextPopup();
    }

    public override void Update()
    {
        if (isOpened && !buttonPressed && !SelectBack && !cbc.isClosed())
        {
            carter.makeMovable();
            NonSelectablePopup.Close();
            NonSelectablePopup.SetReturnStaminaPopup(this);
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !cbc.isClosed() && SelectBack)
        {
            ReturnPopup.Open();
            Close();
        }
    }*/
}