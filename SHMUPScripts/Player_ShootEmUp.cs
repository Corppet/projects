using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float velocity; // speed of player movement
    [SerializeField] GameObject firePoint; // where the projectile spawns
    [SerializeField] GameObject projectile; // projectile object
    [SerializeField] float fireRate; // default player fire rate
    [SerializeField] float rapidfireRate; // player fire rate while powered up
    [SerializeField] float rapidfireTime; // duration of a rapidfire powerup
    [SerializeField] float projectileForce; // speed of the projectile
    [SerializeField] int maxHealth; // player health
    [SerializeField] AudioSource hitSound; // when the player gets hit
    [SerializeField] AudioSource shootSFX; // shoot SFX
    [SerializeField] AudioSource lifeupSFX; // lifeup SFX
    [SerializeField] AudioSource rapidfireSFX; 

    // Other Fields
    Vector2 moveInput;
    float nextShot;
    public int currentHealth;
    bool rapidFire;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing private fields
        nextShot = 0f;
        rapidFire = false;
        currentHealth = maxHealth;
        GameObject.Find("Health Bar").GetComponent<Slider>().maxValue = maxHealth;
        GameObject.Find("Health Bar").GetComponent<Slider>().value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting move key inputs
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Orientation of player sprite
        if (moveInput.x == 0)
        {
            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveInput.x));
        }
        else if (moveInput.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveInput.x));
        }
        else if (moveInput.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveInput.x));
        }

        // Firing Controls
        if (Input.GetButtonDown("Fire1") && Time.time > nextShot)
        {
            StartCoroutine(FireProjectile());
            nextShot = Time.time + fireRate;
        }
    }

    private void FixedUpdate()
    {
        // Movement Controls
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * moveInput.x, velocity * moveInput.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy_ShootEmUp")
        {
            currentHealth -= 2;
            hitSound.Play();
        }
        else if (collision.collider.tag == "EnemyProjectile_ShootEmUp")
        {
            currentHealth--;
            hitSound.Play();
        }
        else if (collision.collider.tag == "LifeUp_ShootEmUp")
        {
            currentHealth++;
            lifeupSFX.Play();
        }
        else if (collision.collider.tag == "RapidFire_ShootEmUp")
        {
            StartCoroutine(TriggerRapidfire());
            rapidfireSFX.Play();
        }

        GameObject.Find("Health Bar").GetComponent<Slider>().value = currentHealth;

        if (currentHealth <= 0)
            LoadAScene.ChangeScene("ShootEmUp End");
    }

    IEnumerator FireProjectile()
    {
        GameObject firedProjectile;
        Vector3 aimVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90f;

        if (rapidFire)
        {
            while (Input.GetButton("Fire1") && rapidFire)
            {
                firedProjectile = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
                firedProjectile.transform.Rotate(new Vector3(0f, 0f, aimAngle));
                firedProjectile.GetComponent<Rigidbody2D>().AddForce(firedProjectile.transform.up * projectileForce, ForceMode2D.Impulse);
                Destroy(firedProjectile, 3f);
                shootSFX.Play();

                yield return new WaitForSeconds(rapidfireRate);
            }
        }
        else
        {
            firedProjectile = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
            firedProjectile.transform.Rotate(new Vector3(0f, 0f, aimAngle));
            firedProjectile.GetComponent<Rigidbody2D>().AddForce(firedProjectile.transform.up * projectileForce, ForceMode2D.Impulse);
            Destroy(firedProjectile, 3f);
            shootSFX.Play();

            yield return null;
        }
    }

    IEnumerator TriggerRapidfire()
    {
        rapidFire = true;

        yield return new WaitForSeconds(rapidfireTime);

        rapidFire = false;
    }
}
