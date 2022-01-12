using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    // Game Objects
    public GameObject scoreMessage;

    // Start is called before the first frame update
    void Start()
    {
        scoreMessage.GetComponent<Text>().text = "You completed your adventure in " + GameManagerScript.getScore() + " moves.";
    }

    // Update is called once per frame
    void Update()
    {
        // Main Menu Keybind
        if (Input.GetKeyDown("escape"))
            LoadAScene.ReturnToMain();
    }
}
