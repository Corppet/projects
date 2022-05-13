using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuManager : MonoBehaviour
{
    [SerializeField] GameObject SkillTree;

    private Animation stAnim;

    private bool closed;
    private UIManager uiMan;

    void Start(){
        uiMan = transform.parent.parent.parent.GetComponent<UIManager>();
        closed = true;
        stAnim = SkillTree.GetComponent<Animation>();
        // assign starting location for the crew managers
        SkillTree.GetComponent<RectTransform>().anchoredPosition 
            = new Vector2(SkillTree.GetComponent<RectTransform>().anchoredPosition.x, 983.0f);
    }

    public bool isClosed(){ return closed; }

    public void ButtonPressed(){
        if(closed)
            Open();
        else
            Close();
    }

    public void Close(){
        uiMan.reopenUI(UIManager.UIType.WeaponTreeUI);
        closed = true;
        stAnim.Play("SkillTreeCloseAnim");
    }

    public void Open(){
        uiMan.openUI(UIManager.UIType.WeaponTreeUI);
        closed = false;
        stAnim.Play("SkillTreeOpenAnim");
    }
}
