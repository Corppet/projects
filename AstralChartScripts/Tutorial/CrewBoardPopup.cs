using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewBoardPopup : PopupObject
{
    /*

    [SerializeField] private CrewBoardPopup NonSelectablePopup;
    [SerializeField] private PointerPopup NonSelectablePointerPopup;
    [SerializeField] CrewButtonController cbc;
    [SerializeField] private bool SelectBack = false;

    private CrewBoardPopup ReturnPopup;
    private PointerPopup ReturnPointerPopup;
    private StaminaPopup ReturnStaminaPopup;

    public bool hover = false;

    public void SetReturnBoardPopup(CrewBoardPopup cbp){
        ReturnPopup = cbp;
    }

    public void SetReturnPointerPopup(PointerPopup pp){
        ReturnPointerPopup = pp;
        hover = true;
    }

    public void SetReturnStaminaPopup(StaminaPopup pp){
        ReturnStaminaPopup = pp;
    }

    public void ButtonPressed()
    {
        if (!buttonPressed && isOpened && !SelectBack)
        {
            NonSelectablePopup.hover = false;
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !cbc.isClosed() && SelectBack)
        {
            if(hover){
                ReturnPointerPopup.Open();
                Close();
                hover = false;
            } else {
                ReturnPopup.Open();
                Close();
                hover = false;
            }
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
            NonSelectablePopup.Close();
            NonSelectablePopup.SetReturnBoardPopup(this);
            NonSelectablePopup.hover = false;
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !cbc.isClosed() && SelectBack)
        {
            if(hover){
                NonSelectablePointerPopup.Open();
                Close();
                hover = false;
            } else {
                ReturnPopup.Open();
                Close();
                hover = false;
            }
        }
    }*/
}