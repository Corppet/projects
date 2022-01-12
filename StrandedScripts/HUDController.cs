using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private Text hpText;
    [SerializeField] private Text timerText;

    // Other Variables
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player Object");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator StartTimer(float time)
    {
        while (time > 0f)
        {
            timerText.text = "" + time;

            yield return new WaitForSeconds(1f);

            time--;
        }

        playerObject.GetComponent<PlayerController>().TriggerLose();
    }

    public void SetHealth(int hp)
    {
        hpText.text = "HP: " + hp;
    }
}
