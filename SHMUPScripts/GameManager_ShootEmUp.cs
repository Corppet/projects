using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] GameObject scoreText; // text object for score
    [SerializeField] GameObject powerupText; // text object for current powerup
    [SerializeField] GameObject playerObject; // player
    [SerializeField] GameObject enemyObject1; // enemy Bub
    [SerializeField] GameObject enemyObject2; // enemy Rolling Nero
    [SerializeField] float waveTime; // time between each wave
    [SerializeField] int startingWaveCount; // number of enemies in the first wave
    [SerializeField] int waveCountUp; // increase next wave count by this amount
    [SerializeField] AudioSource waveSFX; // wave SFX
    [SerializeField] AudioSource neroSFX; // spawn Rolling Nero SFX

    // Other Fields
    GameObject player;
    static int score;
    static string powerup;
    int waveCount;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing fields
        score = 0;
        waveCount = startingWaveCount;

        player = Instantiate(playerObject);
        Bub_ShootEmUp.setPlayerObject(player);
        RollingNero_ShootEmUp.setPlayerObject(player);

        InvokeRepeating("SpawnWave", 0f, waveTime);
        SpawnObject(enemyObject2);
    }

    // Update is called once per frame
    void Update()
    {
        // main menu keybind
        if (Input.GetKeyDown("escape"))
            LoadAScene.ChangeScene("_MainMenu");
    }

    // Changing the player's score
    public void addScore(int x)
    {
        score += x;
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }

    public void subtractScore(int x)
    {
        score -= x;
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }

    public static int getScore()
    {
        return score;
    }

    // Setting the powerup text
    public void changePowerup(string x)
    {
        powerupText.GetComponent<Text>().text = "Powerup: " + x;
    }

    void SpawnObject(GameObject x)
    {
        float randomX;
        float randomY;

        // Random X value
        if (Random.value > .5f)
        {
            randomX = Random.Range(15f, 17f);
        }
        else
        {
            randomX = Random.Range(-17f, -15f);
        }

        // Random Y value
        if (Random.value > .5f)
        {
            randomY = Random.Range(7f, 9f);
        }
        else
        {
            randomY = Random.Range(-9f, -7f);
        }

        Vector3 randomVector = new Vector3(randomX, randomY, 0f);
        Vector3 distanceVector = randomVector - player.transform.position;

        // Spawning object
        if (Mathf.Abs(distanceVector.magnitude) > 5f)
        {
            Instantiate(x, new Vector3(randomX, randomY, 0f), Quaternion.identity);
        }
        else
            SpawnObject(x);
    }

    // Spawn in waves
    public void SpawnWave()
    {
        for (int x = 0; x < waveCount; x++)
        {
            SpawnObject(enemyObject1);
        }

        waveCount += waveCountUp;

        waveSFX.Play();
    }

    // Respawning Rolling Nero
    public void respawnRollingNero(float time)
    {
        StartCoroutine(SpawnAfterDelay(enemyObject2, time));
    }

    IEnumerator SpawnAfterDelay(GameObject x, float time)
    {
        yield return new WaitForSeconds(time);

        SpawnObject(x);
        neroSFX.Play();
    }
}
