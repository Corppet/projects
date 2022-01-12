using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyObjectBehaviour : MonoBehaviour
{
    // Game Objects
    public GameObject scoreObject, upgradeObject;

    // Public Fields
    public int upgradeRate; // Drop an upgrade object every (upgradeRate) items
    public float dropRate; // Item drop rate (seconds)
    public int scoreInterval; // Score interval to change drop rate 
    public float rateIncrease; // Increase drop rate by rateIncrease%
    public float velocityMuliplier; // Increase current velocity by (velocityMuliplier) times

    // Private Fields
    int nextDropScore; // Next score to change drop rate
    int upCount; // Number of items before next powerup drop

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
        nextDropScore = scoreInterval;
        upCount = upgradeRate;
        Invoke("DropItem", dropRate);
        InvokeRepeating("RollDirection", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // Change direction when at the edge of the screen
        if (transform.position.x > 8.5f)
        {
            transform.position = new Vector2(8.49f, transform.position.y);
            GetComponent<Rigidbody2D>().velocity *= -1f;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector2(-8.49f, transform.position.y);
            GetComponent<Rigidbody2D>().velocity *= -1f;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void DropItem()
    {
        if (upCount > 1)
        {
            upCount--;
            Instantiate(scoreObject, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            upgradeRate *= 2;
            upCount = upgradeRate;
            Instantiate(upgradeObject, gameObject.transform.position, Quaternion.identity);
        }

        Invoke("DropItem", dropRate);

        // Changes drop rate and velocity at specific score intervals
        if (GameObject.Find("AppleTreeManager").GetComponent<AppleTree>().GetScore() == nextDropScore && dropRate > .1f)
        {
            GetComponent<Rigidbody2D>().velocity *= velocityMuliplier;
            nextDropScore += scoreInterval;
            dropRate *= 1f - (rateIncrease / 100f);
        }
    }

    void RollDirection()
    {
        if (transform.position.x <= 8f || transform.position.x >= -8f)
        {
            if (Random.value <= .2f)
            {
                GetComponent<Rigidbody2D>().velocity *= -1f;

                if (GetComponent<SpriteRenderer>().flipX)
                    GetComponent<SpriteRenderer>().flipX = false;
                else
                    GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
