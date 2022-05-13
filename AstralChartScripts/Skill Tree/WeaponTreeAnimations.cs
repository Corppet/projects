using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTreeAnimations : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Animation stAnim;

    // Private Variables
    private bool closed;

    void Start()
    {
        closed = true;
    }

    public bool isClosed() { return closed; }

    public void ButtonPressed()
    {
        if (closed)
            Open();
        else
            Close();
    }

    public void Close()
    {
        uiManager.reopenUI(UIManager.UIType.WeaponTreeUI);
        closed = true;
        stAnim.Play("SkillTreeCloseAnim");
    }

    public void Open()
    {
        uiManager.openUI(UIManager.UIType.WeaponTreeUI);
        closed = false;
        stAnim.Play("SkillTreeOpenAnim");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ButtonPressed();
    }
}
