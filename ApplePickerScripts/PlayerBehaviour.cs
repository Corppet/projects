using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    // Game Objects
    public GameObject catcherObject1, catcherObject2, catcherObject3; // Catcher objects
    public GameObject heart1, heart2, heart3; // Heart sprites

    // Audio Sources
    public AudioSource upSound; // Upgrade SFX
    public AudioSource downSound; // Downgrade SFX

    // Fields
    static int lifeCount;
    GameObject[] containers = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        lifeCount = 1;

        Instantiate(catcherObject1, new Vector2(0f, -4f), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Following the mouse's x-position
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y);
    }

    // When the player gains a life
    public void Upgrade()
    {
        switch (lifeCount)
        {
            case 1:
                lifeCount++;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(catcherObject2, new Vector2(transform.position.x, -4f), Quaternion.identity, transform);
                heart1.GetComponent<SpriteRenderer>().enabled = false;
                heart2.GetComponent<SpriteRenderer>().enabled = true;
                upSound.Play();
                containers[0] = null;
                containers[1] = gameObject;
                break;
            case 2:
                lifeCount++;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(catcherObject3, new Vector2(transform.position.x, -4f), Quaternion.identity, transform);
                heart2.GetComponent<SpriteRenderer>().enabled = false;
                heart3.GetComponent<SpriteRenderer>().enabled = true;
                upSound.Play();
                containers[1] = null;
                containers[2] = gameObject;
                break;
            default:
                break;
                
        }
    }

    // When the player loses a life
    public void Downgrade()
    {
        switch (lifeCount)
        {
            case 3:
                lifeCount--;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(catcherObject2, new Vector2(transform.position.x, -4f), Quaternion.identity, transform);
                heart3.GetComponent<SpriteRenderer>().enabled = false;
                heart2.GetComponent<SpriteRenderer>().enabled = true;
                downSound.Play();
                containers[2] = null;
                containers[1] = gameObject;
                break;
            case 2:
                lifeCount--;
                Destroy(transform.GetChild(0).gameObject);
                Instantiate(catcherObject1, new Vector2(transform.position.x, -4f), Quaternion.identity, transform);
                heart2.GetComponent<SpriteRenderer>().enabled = false;
                heart1.GetComponent<SpriteRenderer>().enabled = true;
                downSound.Play();
                containers[1] = null;
                containers[0] = gameObject;
                break;
            case 1:
                AppleTree.GameOver();
                break;
            default:
                break;
        }
    }
}
