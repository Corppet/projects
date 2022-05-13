using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyButtonPopup : PopupObject
{
    /*
    [SerializeField] private BountyButton bb;

    [SerializeField] private OpenBountyBoardPopup NonSelectablePopup;
    [SerializeField] BountyBoardManager bbm;
    [SerializeField] private bool SelectBack = false;

    private OpenBountyBoardPopup ReturnPopup;

    public void SetReturnBoardPopup(OpenBountyBoardPopup obbp){
        ReturnPopup = obbp;
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
            ReturnPopup.Open();
            Close();
        }
    }

    public override IEnumerator BufferNextPopup(float delay){
        tutorialManager.nextPopup();
        yield return null;
    }

    bool once = true;

    public override void Update()
    {
        if(once){
            NonSelectablePopup.SetReturnBoardButtonPopup(this);
            once = false;
        }
        if(bbm.isClosed() && isOpened && !buttonPressed && !SelectBack){
            Close();
            NonSelectablePopup.BoardButtonSet();
            NonSelectablePopup.Open();
            buttonPressed = false;
        }
        if (isOpened && !buttonPressed && !SelectBack && !bbm.isClosed() && bb.getStatus() == BountyStatus.InPursuit)
        {
            NonSelectablePopup.Close();
            NonSelectablePopup.SetReturnBoardButtonPopup(this);
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
        else if(isOpened && !bbm.isClosed() && SelectBack)
        {
            ReturnPopup.Open();
            Close();
        }
    }*/
}

