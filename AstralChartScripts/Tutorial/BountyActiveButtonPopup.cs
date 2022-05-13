using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyActiveButtonPopup : PopupObject
{
    /*
    [SerializeField] private OpenBountyBoardPopup NonSelectablePopup;
    [SerializeField] BountyBoardManager bbm;
    [SerializeField] private bool SelectBack = false;
    [SerializeField] private bool final = false;

    private BountyActiveButtonPopup ReturnPopup;

    public void SetReturnBoardPopup(BountyActiveButtonPopup babp){
        ReturnPopup = babp;
    }

    public void ButtonPressed()
    {
        if (!buttonPressed && isOpened && !SelectBack)
        {
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
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
            NonSelectablePopup.SetReturnBoardActiveButtonPopup(this);
            once = false;
        }
        if(bbm.isClosed() && isOpened && !buttonPressed && !SelectBack && !final){
            Close();
            NonSelectablePopup.BoardActiveSet();
            NonSelectablePopup.Open();
            buttonPressed = false;
        } else if(bbm.isClosed() && isOpened && !buttonPressed && !SelectBack && final){
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = false;
        }
    }*/
}
