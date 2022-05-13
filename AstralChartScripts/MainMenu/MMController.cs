using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMController : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private List<Button> menuButtons;
    [SerializeField] private List<Button> levelButtons;

    public void Play()
    {
        StartCoroutine(BeginPlay());
    }

    public void LevelSelect()
    {
        StartCoroutine(PanelSelect(false));
    }

    public void MenuSelect()
    {
        StartCoroutine(PanelSelect(true));
    }

    private IEnumerator PanelSelect(bool isMenu)
    {
        yield return new WaitForSeconds(2f);

        foreach (Button button in menuButtons)
            button.interactable = isMenu;
        foreach (Button button in levelButtons)
            button.interactable = !isMenu;
    }

    public void Theatre()
    {
        StartCoroutine(BeginTheatre());
    }

    private IEnumerator BeginTheatre()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Theatre");
    }

    private IEnumerator BeginPlay()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Loading Screen");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Intro Scene");
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        StartCoroutine(HideCredits());
    }

    private IEnumerator HideCredits()
    {
        yield return new WaitForSeconds(2.0f);
        creditsPanel.SetActive(false);
    }

    public void Start()
    {
        creditsPanel.SetActive(false);

        MenuSelect();
    }
}
