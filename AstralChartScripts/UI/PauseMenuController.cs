using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private Animator pmAnim;
    private bool closed;

    [SerializeField] GameObject PauseMenu;
    [SerializeField] Image OpenMenuButton;
    [SerializeField] Sprite[] pauseAndResume;
    [SerializeField] UIManager uiManager;


    void Start(){
        closed = true;
        pmAnim = PauseMenu.GetComponent<Animator>();
        PauseMenu.GetComponent<RectTransform>().anchoredPosition 
            = new Vector2(PauseMenu.GetComponent<RectTransform>().anchoredPosition.y, 1382.0f);
        Time.timeScale = 1;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && uiManager.allMainUIClosed(UIManager.UIType.PauseMenu)){
            ButtonPressed();
        }
    }

    public void ButtonPressed(){
        if(closed)
            Pause();
        else
            ResumeGame();
    }

    void Pause(){
        Time.timeScale = 0;
        closed = false;
        OpenMenuButton.sprite = pauseAndResume[1];
        pmAnim.SetTrigger("Pause");
    }
    
    void ResumeGame(){
        Time.timeScale = 1;
        closed = true;
        OpenMenuButton.sprite = pauseAndResume[0];
        pmAnim.SetTrigger("Resume");
    }

    public void LoadNormal(){
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadTutorial(){
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("Intro Scene");
    }

    public void Quit(){
        Application.Quit();
    }
}
