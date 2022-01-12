using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppleTree : MonoBehaviour
{
    // Game Objects
    public GameObject scoreObject, enemyObject, catcherObject1, catcherObject2, catcherObject3, scoreText;

    // Audio Sources
    public AudioSource collectSound;
    
    // Fields
    public static int score; // Player Score
    List<GameObject> potions = new List<GameObject>(); // List of potions

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Instantiate(enemyObject, new Vector2(0f, 4f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("_MainMenu");
    }

    // Increase the player's score
    public void IncreaseScore(int x)
    {
        collectSound.Play();
        score += x;
        scoreText.GetComponent<Text>().text = "" + score;
    }

    // Decrease the player's score
    public void DecreaseScore(int x)
    {
        score -= x;
        scoreText.GetComponent<Text>().text = "" + score;
    }

    // Get the player's score
    public int GetScore()
    {
        return score;
    }
    public void AddItem(GameObject x)
    {
        potions.Add(x);
    }

    public void DeleteItem(GameObject x)
    {
        potions.Remove(x);
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("AppleTreeGameOver");
    }
}
