using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewButtonController : MonoBehaviour
{
    [SerializeField] GameObject ShipManager;
    [SerializeField] GameObject CrewCard;
    public GameObject openButton;

    private Animation smAnim;
    private Animation ccAnim;
    private UIManager uiMan;

    private bool closed;
    private bool examineClosed;

    void Start(){
        closed = true;
        examineClosed = true;
        smAnim = ShipManager.GetComponent<Animation>();
        ccAnim = CrewCard.GetComponent<Animation>();
        uiMan = transform.parent.parent.GetComponent<UIManager>();
        // assign starting location for the crew managers
        CrewCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-744.37f, CrewCard.GetComponent<RectTransform>().anchoredPosition.y);
        ShipManager.GetComponent<RectTransform>().anchoredPosition = new Vector2(1516.0f, ShipManager.GetComponent<RectTransform>().anchoredPosition.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            ButtonPressed();
        if (Input.GetKeyDown(KeyCode.Escape) && !isClosed())
            Close();
    }

    public bool isClosed(){ return closed; }

    public void ButtonPressed(){
        if (closed)
            Open();
        else
            Close();
    }

    public void OpenCrewCard(){
        ccAnim.Play("CrewCardOpenAnim");
        examineClosed = false;
    }

    public void CloseCrewCard(){
        ccAnim.Play("CrewCardCloseAnim");
        examineClosed = true;
    }

    public void Close(){
        uiMan.reopenUI(UIManager.UIType.CrewBoard);
        closed = true;
        if(!examineClosed)
            ccAnim.Play("CrewCardCloseAnim");
        smAnim.Play("CrewManagerCloseAnim");
    }

    public void Open(){
        uiMan.openUI(UIManager.UIType.CrewBoard);
        closed = false;
        smAnim.Play("CrewManagerOpenAnim");
    }
}
