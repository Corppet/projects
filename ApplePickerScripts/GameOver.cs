using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Game Objects
    public GameObject scoreText;

    // Fields
    static int score;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<Text>().text = "Score: " + AppleTree.score;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("_MainMenu");
    }
}
