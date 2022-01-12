using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bub_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float moveSpeed; // move speed
    [SerializeField] float detectRadius; // range which Bub can detect the player
    [SerializeField] int hitPoints; // starting health
    [SerializeField] float powerupRate; // chance to spawn powerup on death (percent)
    [SerializeField] GameObject lifeObject; // LifeUp powerup
    [SerializeField] GameObject rapidfireObject; // Rapidfire powerup
    [SerializeField] GameObject hitEffect; // death animation

    // Other Fields
    static GameObject playerObject;
    bool hurt;

    // Start is called before the first frame update
    void Start()
    {
        hurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetVector;

        targetVector = playerObject.transform.position - transform.position;

        // Move when player is in range
        if (targetVector.magnitude <= detectRadius && !hurt)
        {
            Debug.DrawLine(playerObject.transform.position, transform.position);

            if (!Physics2D.Linecast(playerObject.transform.position, transform.position + (Vector3)targetVector.normalized, ~LayerMask.GetMask("Player")))
            {
                Debug.DrawLine(playerObject.transform.position, transform.position, Color.red);
                //transform.position = Vector2.Lerp(transform.position, playerObject.transform.position, Time.deltaTime * moveSpeed);
                // ^^ line works if you remove line below, but orientation of sprite will not change.
                gameObject.GetComponent<Rigidbody2D>().velocity = targetVector.normalized * moveSpeed;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity *= 0;
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity *= 0;
        }

        // Orientation and animation of object sprite
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player_ShootEmUp")
        {
            StartCoroutine(GotHit(hitPoints));
        }
        else if (collision.collider.tag == "FriendlyProjectile_ShootEmUp")
        {
            StartCoroutine(GotHit(1));
        }
    }

    IEnumerator GotHit(int damage)
    {
        if (hitPoints - damage <= 0)
        {
            // roll for powerup
            if (Random.value < powerupRate / 100f)
            {
                if (Random.value > .5f)
                    Instantiate(lifeObject, transform.position, Quaternion.identity);
                else
                    Instantiate(rapidfireObject, transform.position, Quaternion.identity);
            }

            // death effect
            GameObject hitObject = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitObject, 2f);
            GameObject.Find("Game Manager").GetComponent<GameManager_ShootEmUp>().addScore(1);
            Destroy(gameObject);
        }
        else
        {
            hitPoints -= damage;
            gameObject.GetComponent<Rigidbody2D>().velocity *= 0;

            hurt = true;
            gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            yield return new WaitForSeconds(.5f);
            hurt = false;
        }

        yield return null;
    }

    public static void setPlayerObject(GameObject player)
    {
        playerObject = player;
    }
}
