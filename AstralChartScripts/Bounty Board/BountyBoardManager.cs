using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

/// <summary>
/// Core management of all bounties.
/// </summary>
public class BountyBoardManager : MonoBehaviour
{
    // Adjustable Values
    public int reputation;
    [Tooltip("Keybind to open/close the Bounty Board.")]
    [SerializeField] KeyCode toggleKey;

    [Space]

    // Serialized Fields
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private Animation stAnim;
    [SerializeField] private GameObject previewPlayer;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject healthPanel;
    //[SerializeField] private TMP_Dropdown factionDropdown;
    [SerializeField] private Button pursueButton;
    [SerializeField] private Button claimButton;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text requiredReputationText;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private TMP_Text prerequisiteText;
    [SerializeField] private TMP_Text reputationText;
    [SerializeField] private Image previewImage;
    public GameObject openButton;

    [SerializeField] private GameObject TutorialTree;
    [SerializeField] private GameObject ASTree;
    [SerializeField] private GameObject BUTree;
    [SerializeField] private GameObject FORTree;
    [SerializeField] private GameObject MRDTree;
    [SerializeField] private Button TutorialButton;
    [SerializeField] private Button ASButton;
    [SerializeField] private Button BUButton;
    [SerializeField] private Button FORButton;
    [SerializeField] private Button MRDButton;

    [SerializeField] private DialogueInitiator dialogue;
    [SerializeField] private Pointer questPointer;

    // Private Variables
    private ShipHealth shipHealth;
    private VideoPlayer videoPlayer;
    private BountyButton selected;
    public BountyButton currentPursued;
    private Faction selectedFaction;
    private bool closed;
    private bool tutorialOn;

    public void setReputation(int value)
    {
        reputation = value;
        reputationText.text = value.ToString();
    }

    /// <summary>
    /// Set the currently selected bounty in the Bounty Board.
    /// </summary>
    /// <param name="button">
    /// Selected button.
    /// </param>
    public void setSelected(GameObject button)
    {
        selected = button.GetComponent<BountyButton>();
        BountyObject bounty = selected.bountyDetails;

        if (selected != currentPursued && selected.status == BountyStatus.Available)
        {
            if (selected.prerequisite != null)
            {
                pursueButton.gameObject.SetActive(true);
                if (currentPursued == null && selected.prerequisite.status == BountyStatus.Claimed
                    && selected.status == BountyStatus.Available)
                    pursueButton.interactable = true;
                else
                    pursueButton.interactable = false;
                claimButton.gameObject.SetActive(false);

                if (selected.prerequisite.status != BountyStatus.Claimed)
                {
                    if (selected.prerequisite.prerequisite == null ||
                        selected.prerequisite.prerequisite.status == BountyStatus.Claimed)
                        prerequisiteText.text = "Requires " + selected.prerequisite.bountyDetails.bountyName + ".";
                    else
                        prerequisiteText.text = "Requires ???.";
                }
                else
                    prerequisiteText.text = "";
            }
            else
            {
                pursueButton.gameObject.SetActive(true);
                if (currentPursued == null && selected.status == BountyStatus.Available)
                    pursueButton.interactable = true;
                else
                    pursueButton.interactable = false;
                claimButton.gameObject.SetActive(false);

                prerequisiteText.text = "";
            }
        }
        else
        {
            pursueButton.gameObject.SetActive(false);
            claimButton.gameObject.SetActive(true);
            if (selected.entity == null && selected.status == BountyStatus.InPursuit)
                claimButton.interactable = true;
            else
                claimButton.interactable = false;
        }

        if (selected.prerequisite == null || selected.prerequisite.status == BountyStatus.Claimed)
        {
            titleText.text = bounty.bountyName;
            descriptionText.text = bounty.description;
            requiredReputationText.text = "Required Reputation: " + bounty.requiredReputation;
            if (selected.bountyDetails.weaponReward != null)
            {
                rewardText.text = "Reward: " + bounty.weaponReward.upgradeName;
                previewImage.sprite = bounty.weaponReward.uiImage;
                previewImage.gameObject.SetActive(true);
            }
            else
            {
                rewardText.text = "";
                previewImage.gameObject.SetActive(false);
            }
        }
        else
        {
            titleText.text = "???";
            descriptionText.text = "";
            requiredReputationText.text = "";
            rewardText.text = "";
            previewImage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Pursue the selected bounty.
    /// </summary>
    public void pursue()
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.Pursue);
        selected.status = BountyStatus.InPursuit;
        currentPursued = selected;
        pursueButton.interactable = false;
        selected.updateDetails();
        Close(false);
        if(selected.entity == null)
            return;
        questPointer.BeginTracking(selected.entity);
        StartCoroutine(dialogue.StartDialogue(currentPursued.bountyDetails.start_interaction));
    }

    public bool pursueStatus(){ return (selected.status == BountyStatus.InPursuit || selected.status == BountyStatus.Claimed); }
    public bool claimedStatus(){ return (selected.status == BountyStatus.Claimed); }

    /// <summary>
    /// Claim the reward of the selected bounty.
    /// </summary>
    public void claim()
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.Claim);
        selected.status = BountyStatus.Claimed;
        setReputation(reputation + 1);
        if (selected.bountyDetails.weaponReward != null)
            foreach (Option option in shopManager.weaponUpgrades)
                if (selected.bountyDetails.weaponReward.Equals((WeaponUpgrade)option.upgrade))
                {
                    option.purchased = true;
                    break;
                }
        shopManager.updateShop();
        claimButton.interactable = false;
        questPointer.StopTracking();
        rewardPanel.SetActive(true);
        foreach (BountyButton button in GetComponentsInChildren<BountyButton>())
            button.updateDetails();
    }
    
    /// <summary>
    /// OnPointerEnter/Exit function for restore health button.
    /// </summary>
    /// <param name="pointerInside">
    /// True if using OnPointerEnter. False otherwise.
    /// </param>
    public void previewRestore(bool pointerInside)
    {
        if (pointerInside)
        {
            healthPanel.GetComponentInChildren<TMP_Text>().text = "Restores " 
                + currentPursued.bountyDetails.restoreHealthReward + " health to your ship.";
            healthPanel.SetActive(true);
        }
        else
            healthPanel.SetActive(false);
    }

    /// <summary>
    /// OnClick function to select the restore health reward.
    /// </summary>
    public void selectRestore()
    {
        shipHealth.AddHealth(currentPursued.bountyDetails.restoreHealthReward);
        rewardPanel.SetActive(false);
        Close(false);
        StartCoroutine(dialogue.StartDialogue(currentPursued.bountyDetails.end_interaction));
        currentPursued = null;
    }

    /// <summary>
    /// OnPointerEnter/Exit function for restore health button.
    /// </summary>
    /// <param name="pointerInside">
    /// True if using OnPointerEnter. False otherwise.
    /// </param>
    public void previewMaxIncrease(bool pointerInside)
    {
        if (pointerInside)
        {
            healthPanel.GetComponentInChildren<TMP_Text>().text = "Increases your ship's max health by "
                + currentPursued.bountyDetails.maxHealthReward + " hitpoints.";
            healthPanel.SetActive(true);
        }
        else
            healthPanel.SetActive(false);
    }

    /// <summary>
    /// OnClick function to select the max health reward.
    /// </summary>
    public void selectMaxIncrease()
    {
        shipHealth.maxHealth += currentPursued.bountyDetails.maxHealthReward;
        shipHealth.AddHealth(selected.bountyDetails.maxHealthReward);
        rewardPanel.SetActive(false);
        Close(false);
        StartCoroutine(dialogue.StartDialogue(currentPursued.bountyDetails.end_interaction));
        currentPursued = null;
    }

    /// <summary>
    /// Set which bounty board to show.
    /// </summary>
    public void setFaction(int faction)
    {
        TutorialTree.SetActive(false);
        ASTree.SetActive(false);
        BUTree.SetActive(false);
        FORTree.SetActive(false);
        MRDTree.SetActive(false);
        TutorialButton.interactable = true;
        ASButton.interactable = true;
        BUButton.interactable = true;
        FORButton.interactable = true;
        MRDButton.interactable = true;

        switch (faction)
        {
            case 0:
                TutorialTree.SetActive(true);
                TutorialButton.interactable = false;
                selectedFaction = Faction.Neutral;
                break;
            case 1:
                ASTree.SetActive(true);
                ASButton.interactable = false;
                selectedFaction = Faction.AscendantSystems;
                break;
            case 2:
                BUTree.SetActive(true);
                BUButton.interactable = false;
                selectedFaction = Faction.BastionUnited;
                break;
            case 3:
                FORTree.SetActive(true);
                FORButton.interactable = false;
                selectedFaction = Faction.FollowingOfReunion;
                break;
            case 4:
                MRDTree.SetActive(true);
                MRDButton.interactable = false;
                selectedFaction = Faction.MatrioshkaResearchDivision;
                break;
        }
    }

    /// <summary>
    /// OnPointerEnter function to trigger video preview.
    /// </summary>
    public void startPreview()
    {
        if (selected == null || selected.bountyDetails.weaponReward == null) return;

        WeaponUpgrade weapon = selected.bountyDetails.weaponReward;
        if (weapon.previewClip != null)
        {
            videoPlayer.clip = weapon.previewClip;
            previewPlayer.SetActive(true);
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// OnPointerExit function to stop video preview.
    /// </summary>
    public void stopPreview()
    {
        previewPlayer.SetActive(false);
        videoPlayer.Stop();
    }

    /// <summary>
    /// Returns true if the interface is closed. False otherwise.
    /// </summary>
    /// <returns></returns>
    public bool isClosed() { return closed; }

    /// <summary>
    /// Toggle open/close of the interface.
    /// </summary>
    public void ButtonPressed()
    {
        if(closed)
            Open();
        else
            Close();
    }

    /// <summary>
    /// Open the interface.
    /// </summary>
    public void Open()
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.OpenBountyBoard);
        uiManager.openUI(UIManager.UIType.BountyBoard);
        closed = false;
        stAnim.Play("BountyBoardOpen");
        openButton.SetActive(false);
    }

    /// <summary>
    /// Close the interface.
    /// </summary>
    public void Close(bool openDialogue = true)
    {
        if(tutorialOn) TutorialManager.instance.DisplayTutorialPrompt_ReferenceCall(TutorialEvent.CloseBountyBoard);
        uiManager.reopenUI(UIManager.UIType.BountyBoard, openDialogue);
        closed = true;
        stAnim.Play("BountyBoardClose");
        openButton.SetActive(true);
    }

    private void Start()
    {
        videoPlayer = previewPlayer.GetComponentInChildren<VideoPlayer>();
        shipHealth = playerShip.GetComponentInChildren<ShipHealth>();
        setReputation(reputation);
        setFaction(0);
        titleText.text = "";
        descriptionText.text = "";
        requiredReputationText.text = "";
        rewardText.text = "";
        previewImage.gameObject.SetActive(false);
        prerequisiteText.text = "";
        pursueButton.gameObject.SetActive(true);
        pursueButton.interactable = false;
        claimButton.gameObject.SetActive(false);
        previewPlayer.SetActive(false);
        rewardPanel.SetActive(false);
        closed = true;
        // check if tutorial is on
        tutorialOn = (PlayerPrefs.GetInt("Tutorial") == 0);
    }

    public void FinishTutorial(){ tutorialOn = false; }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            ButtonPressed();
        if (Input.GetKeyDown(KeyCode.Escape) && !isClosed())
            Close();
    }
}