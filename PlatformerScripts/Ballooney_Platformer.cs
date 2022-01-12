using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballooney_Platformer : MonoBehaviour
{
    // Adjustable Fields
    [SerializeField] float moveVelocity;
    [SerializeField] float bounceForce;

    [Space]

    // Other Serialized Fields
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject hitEffect;

    // Other Fields
    GameObject player;
    Vector2 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Object");
    }

    // Update is called once per frame
    void Update()
    {
        targetVector = player.transform.position - transform.position;

        // Movement
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveVelocity);

        // Animation Controls
        if (targetVector.x > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (targetVector.x < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player_Platformer" || collision.collider.tag == "FriendlyProjectile_Platformer")
        {
            if (collision.collider.tag == "Player_Platformer")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, bounceForce), ForceMode2D.Impulse);
            }

            GameObject hitObject = Instantiate(hitEffect, transform.position, Quaternion.identity);
            hitObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 2f;
            Destroy(hitObject, .5f);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Platformer" || collision.tag == "FriendlyProjectile_Platformer")
        {
            if (collision.tag == "Player_Platformer")
            {
                collision.gameObject.GetComponent<Player_Platformer>().changeHealth(-1);
            }

            GameObject hitObject = Instantiate(hitEffect, transform.position, Quaternion.identity);
            hitObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 2f;
            Destroy(hitObject, .5f);
            Destroy(gameObject);
        }
    }
}
