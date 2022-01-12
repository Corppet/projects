using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Platformer : MonoBehaviour
{
    // Adjustable Fields
    [SerializeField] float BallooneySpawnRate; // Ballooney spawn rate (seconds)

    // Other Serialized Fields
    [SerializeField] GameObject mainCamera; // Main Camera
    [SerializeField] GameObject player; // Player Object
    [SerializeField] GameObject Ballooney; // Ballooney prefab

    // Other Fields

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBallooney", 2f, BallooneySpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadAScene.ChangeScene("_MainMenu");
        }
    }
    
    void SpawnBallooney()
    {
        Vector3 randomVector;

        // Getting a random vector
        if (Random.value > .5f)
        {
            randomVector = new Vector3(mainCamera.transform.position.x + mainCamera.GetComponent<Camera>().orthographicSize * (16f / 9f) + 2f,
                Random.Range(transform.position.y - mainCamera.GetComponent<Camera>().orthographicSize, transform.position.y + mainCamera.GetComponent<Camera>().orthographicSize), 0f);
        }
        else
        {
            randomVector = new Vector3(mainCamera.transform.position.x - mainCamera.GetComponent<Camera>().orthographicSize * (16f / 9f) - 2f,
                Random.Range(transform.position.y - mainCamera.GetComponent<Camera>().orthographicSize, transform.position.y + mainCamera.GetComponent<Camera>().orthographicSize), 0f);
        }

        // Spawning object
        if (Mathf.Abs(randomVector.x - player.transform.position.x) > mainCamera.GetComponent<Camera>().orthographicSize * (16f / 9f))
        {
            Instantiate(Ballooney, randomVector, Quaternion.identity);
        }
        else
            SpawnBallooney();
    }
}
