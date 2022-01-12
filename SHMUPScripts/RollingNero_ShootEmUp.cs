using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingNero_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float moveSpeed; // move speed
    [SerializeField] GameObject projectile; // projectile object
    [SerializeField] float fireRate; // projectile fire rate
    [SerializeField] float projectileForce; // projectile force
    [SerializeField] int spreadCount; // number of projectiles fired each time
    [SerializeField] float respawnTime; // respawn timer
    [SerializeField] GameObject hitEffect; // death animation
    [SerializeField] AudioSource shootSFX; // shoot SFX

    // Other Fields
    Rigidbody2D rb;
    float nextShot;
    static GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing fields
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, moveSpeed);
        nextShot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sprite orientation
        if (rb.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        Debug.DrawLine(transform.position, playerObject.transform.position);
        // Firing Controls
        if (Time.time > nextShot)
        {
            StartCoroutine(FireProjectile());
            nextShot = Time.time + fireRate;
        }
    }

    IEnumerator FireProjectile()
    {
        GameObject firedProjectile;
        Vector3 aimVector = (playerObject.transform.position - transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90f;

        for (int x = 0; x < spreadCount; x++)
        {
            firedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            firedProjectile.transform.Rotate(new Vector3(0f, 0f, aimAngle + x * (360f / spreadCount)));
            firedProjectile.GetComponent<Rigidbody2D>().AddForce(firedProjectile.transform.up * projectileForce, ForceMode2D.Impulse);
        }

        shootSFX.Play();

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player_ShootEmUp")
        {
            GameObject.Find("Game Manager").GetComponent<GameManager_ShootEmUp>().respawnRollingNero(respawnTime);
            GameObject hitObject = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitObject, 2f);
            Destroy(gameObject);
        }
    }

    public static void setPlayerObject(GameObject player)
    {
        playerObject = player;
    }
}
