using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] GameObject scoreObject; // score text
    [SerializeField] GameObject highscoreObject; // high score text

    // Other Fields
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameManager_ShootEmUp.getScore();
        scoreObject.GetComponent<Text>().text = "Score: " + score;
        highscoreObject.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highscoreObject.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            LoadAScene.ChangeScene("_MainMenu");
    }
}
