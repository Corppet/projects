using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Management for all UI interfaces.
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// All the interfaces in the game.
    /// </summary>
    public enum UIType
    {
        BountyBoard,
        CrewBoard,
        UpgradeStation,
        WeaponUI,
        DialougeUI,
        PointerUI,
        WeaponTreeUI,
        PauseMenu
    }

    // Serialized Fields
    [SerializeField] private BountyBoardManager bountyBoard;
    //[SerializeField] private CrewButtonController crewBoard;
    [SerializeField] private ShopManager shop;
    [SerializeField] private DialogueInitiator dialogue;
    [SerializeField] private WeaponUIManager weapon;
    [SerializeField] private GameObject Pointer;
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject playerHP;

    /// <summary>
    /// Checks if all other interfaces are closed except one.
    /// </summary>
    /// <param name="except">
    /// The interface to consider as an exception.
    /// </param>
    /// <returns></returns>
    public bool allMainUIClosed(UIType except)
    {
        switch (except)
        {
            case UIType.BountyBoard:
                return weapon.isClosed() && shop.isClosed() && !Pointer.activeSelf;
            //case UIType.CrewBoard:
            //    return weapon.isClosed() && bountyBoard.isClosed() && shop.isClosed() && !Pointer.activeSelf;
            case UIType.WeaponUI:
                return bountyBoard.isClosed() && shop.isClosed() && !Pointer.activeSelf;
            case UIType.UpgradeStation:
                return weapon.isClosed() && bountyBoard.isClosed() && !Pointer.activeSelf;
            case UIType.PauseMenu:
                return bountyBoard.isClosed() && shop.isClosed();
            default:
                return false;
        }
    }

    /// <summary>
    /// Returns all UI to their normal state (active but closed).
    /// </summary>
    /// <param name="ui">
    /// The interface calling this function.
    /// </param>
    public void reopenUI(UIType ui, bool openDialogue = true)
    {
        if (allMainUIClosed(ui))
        {
            weapon.Open();
            Pointer.SetActive(true);
            minimap.SetActive(true);
            playerHP.SetActive(true);
            if (openDialogue)
                StartCoroutine(dialogue.OpenAll());
            bountyBoard.openButton.SetActive(true);
            shop.openButton.SetActive(true);
            //crewBoard.openButton.SetActive(true);
        }
    }

    /// <summary>
    /// Closes all other interfaces but the one to open.
    /// </summary>
    /// <param name="ui">
    /// The interface that is opening.
    /// </param>
    public void openUI(UIType ui)
    {
        switch (ui)
        {
            case UIType.BountyBoard:
                // Close other UIs
                //if (!crewBoard.isClosed()) { crewBoard.Close(); }
                //crewBoard.openButton.SetActive(false);
                if (!weapon.isClosed()) { weapon.Close(); }
                if (!shop.isClosed()) { shop.Close(); }
                shop.openButton.SetActive(false);
                Pointer.SetActive(false);
                minimap.SetActive(false);
                playerHP.SetActive(false);
                StartCoroutine(dialogue.CloseAll());
                break;
            //case UIType.CrewBoard:
            //    // Close other UIs
            //    if (!weapon.isClosed()) { weapon.Close(); }
            //    Pointer.SetActive(false);
            //    minimap.SetActive(false);
            //    if (!bountyBoard.isClosed()) { bountyBoard.Close(); }
            //    bountyBoard.openButton.SetActive(false);
            //    if (!shop.isClosed()) { shop.Close(); }
            //    StartCoroutine(dialogue.CloseAll());
            //    break;
            case UIType.DialougeUI:
                // Close other UIs
                if (!weapon.isClosed()) { weapon.Close(); }
                Pointer.SetActive(false);
                minimap.SetActive(false);
                playerHP.SetActive(false);
                if (!bountyBoard.isClosed()) { bountyBoard.Close(); }
                bountyBoard.openButton.SetActive(false);
                //if (!crewBoard.isClosed()) { crewBoard.Close(); }
                //crewBoard.openButton.SetActive(false);
                if (!shop.isClosed()) { shop.Close(); }
                shop.openButton.SetActive(false);
                StartCoroutine(dialogue.CloseAll());
                break;
            case UIType.UpgradeStation:
                // Close all other UIs
                if (!weapon.isClosed()) { weapon.Close(); }
                Pointer.SetActive(false);
                minimap.SetActive(false);
                playerHP.SetActive(false);
                if (!bountyBoard.isClosed()) { bountyBoard.Close(); }
                bountyBoard.openButton.SetActive(false);
                //if (!crewBoard.isClosed()) { crewBoard.Close(); }
                //crewBoard.openButton.SetActive(false);
                StartCoroutine(dialogue.CloseAll());
                break;
        }
    }
}