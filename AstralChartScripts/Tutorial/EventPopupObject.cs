using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPopupObject : MonoBehaviour
{
    /*
    // Public Variables
    public bool buttonPressed { get; set; }

    // Serialized Fields
    [Tooltip("How long the popup should stay up after the button is pressed.")]
    [SerializeField] protected float triggerDelay;
    [Tooltip("Key that triggers the next popup.")]
    [SerializeField] protected KeyCode triggerKey;

    [Space]

    [SerializeField] protected Animation openAndClose;
    [SerializeField] protected TutorialManager tutorialManager;

    // Protected Variables
    protected bool isOpened;

    public void Open()
    {
        gameObject.SetActive(true);

        if (!isOpened)
        {
            openAndClose.Play("PopupOpen");

            isOpened = true;
        }
    }

    public void Close()
    {
        if (isOpened)
        {
            openAndClose.Play("PopupClose");

            isOpened = false;
        }

        gameObject.SetActive(false);
    }

    public virtual IEnumerator BufferNextPopup(float delay)
    {
        yield return new WaitForSeconds(delay);

        //tutorialManager.nextEventPopup();
    }

    public void Awake()
    {
        isOpened = false;
        buttonPressed = false;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(triggerKey) || !buttonPressed)
        {
            StartCoroutine(BufferNextPopup(triggerDelay));
            buttonPressed = true;
        }
    }*/
}
