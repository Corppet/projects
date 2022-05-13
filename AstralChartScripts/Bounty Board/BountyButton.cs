using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Selectable bounty objects.
/// </summary>
[RequireComponent(typeof(Button))]
public class BountyButton : MonoBehaviour
{
    // Adjustable Values
    public GameObject entity;
    public BountyObject bountyDetails;
    public BountyButton prerequisite;
    [HideInInspector] public BountyStatus status;

    [Space]

    // Other Serialized Fields
    [SerializeField] protected BountyBoardManager bountyManager;
    [SerializeField] protected Image bountyImage;
    [SerializeField] protected TMP_Text titleText;
    [SerializeField] protected GameObject lockIcon;

    private bool tutorialOn;

    public CustomEvent getEvent() { return bountyDetails.scripedEvent; }
    public CustomEventTypes getEventType() { return bountyDetails.eventOrigin; }

    /// <summary>
    /// Update Button/GameObject properties.
    /// </summary>
    public void updateDetails()
    {
        if (status == BountyStatus.InPursuit)
            bountyImage.color = Color.red;
        else if (status == BountyStatus.Claimed)
            bountyImage.color = Color.blue;

        if (prerequisite == null || prerequisite.status == BountyStatus.Claimed)
        {
            titleText.text = bountyDetails.bountyName;
            lockIcon.SetActive(false);
        }
        else
        {
            titleText.text = "???";
            lockIcon.SetActive(true);
        }
    }

    public BountyStatus getStatus(){ return status; }

    /// <summary>
    /// Set currently selected bounty in BountyBoardManager.
    /// </summary>
    public void selectBounty()
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.UsingBountyBoard);
        bountyManager.setSelected(gameObject);
    }

    private void Awake()
    {
        tutorialOn = (PlayerPrefs.GetInt("Tutorial") == 0);
        status = BountyStatus.Available;
        updateDetails();
    }
}
