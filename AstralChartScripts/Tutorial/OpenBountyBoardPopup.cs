using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBountyBoardPopup : PopupObject
{
    /*
    [SerializeField] private OpenBountyBoardPopup NonSelectablePopup;
    [SerializeField] BountyBoardManager bbm;
    [SerializeField] private bool SelectBack = false;

    private OpenBountyBoardPopup ReturnPopup;
    private BountyButtonPopup ReturnButtonPopup;
    private BountyActiveButtonPopup ReturnActivePopup;
    bool button = false;
    bool active = false;

    public void SetReturnBoardPopup(OpenBountyBoardPopup obbp){
        ReturnPopup = obbp;
    }

    public void SetReturnBoardButtonPopup(BountyButtonPopup bbp){
        ReturnButtonPopup = bbp;
    }

    public void BoardButtonSet(){ button = true; }
    public void BoardActiveSet(){ active = true; }

    public void SetReturnBoardActiveButtonPopup(BountyActiveButtonPopup babp){
        ReturnActivePopup = babp;
    }

    public void ButtonPressed()
    {
        if (!buttonPressed && isOpened && !SelectBack)
        {
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !bbm.isClosed() && SelectBack)
        {
            if(active){
                active = false;
                ReturnActivePopup.Open();
                Close();
            } else if(button){
                button = false;
                ReturnButtonPopup.Open();
                Close();
            } else {
                ReturnPopup.Open();
                Close();
            }
        }
    }

    public override IEnumerator BufferNextPopup(float delay){
        for(int x = 0; x < 100; x++){
            yield return new WaitForSeconds(delay/100.0f);
            if(bbm.isClosed()){
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
        if (isOpened && !buttonPressed && !SelectBack && !bbm.isClosed())
        {
            NonSelectablePopup.Close();
            NonSelectablePopup.SetReturnBoardPopup(this);
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !bbm.isClosed() && SelectBack)
        {
            if(active){
                active = false;
                ReturnActivePopup.Open();
                Close();
            } else if(button){
                button = false;
                ReturnButtonPopup.Open();
                Close();
            } else {
                ReturnPopup.Open();
                Close();
            }
        }
    }*/

}
