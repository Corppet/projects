using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Platformer : MonoBehaviour
{
    // Adjustable Fields
    [SerializeField] float moveVelocity; // horizontal move speed
    [SerializeField] float jumpForce; // jump force
    [SerializeField] float fireRate; // default player fire rate
    [SerializeField] float projectileForce; // impulsive force inflicted on player-instantiated projetiles
    [SerializeField] int maxHealth; // player health

    [Space]

    // Other Serialized Fields
    [SerializeField] Rigidbody2D rb; // player object rigidbody
    [SerializeField] Animator anim; // player object animator
    [SerializeField] Collider2D groundedBox; // grounded trigger collider
    [SerializeField] GameObject projectile; // shooting projectile
    [SerializeField] GameObject firePoint; // where the player's bullets will instantiate
    [SerializeField] AudioSource shootSFX; // shooting SFX
    [SerializeField] AudioSource hitSound; // SFX when you get hit
    [SerializeField] AudioSource jumpSFX;


    // Other Fields
    Vector2 moveInput;
    bool hurtState;
    bool grounded;
    float nextShot;
    int currentHealth;
    Vector2 startPos;


    // Start is called before the first frame update
    void Start()
    {
        // Initializing variables
        hurtState = false;
        nextShot = 0f;
        currentHealth = maxHealth;
        startPos = transform.position;
        GameObject.Find("Health Bar").GetComponent<Slider>().maxValue = maxHealth;
        GameObject.Find("Health Bar").GetComponent<Slider>().value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded Check
        if (groundedBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // Horizontal Movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveInput.x, 0f, 0f) * Time.deltaTime * moveVelocity;

        // Animation Controls
        if (grounded)
        {
            anim.SetFloat("Move Speed", Mathf.Abs(moveInput.x) * moveVelocity * .5f);
            anim.SetFloat("Y-Velocity", 0f);
        }
        else
        {
            anim.SetFloat("Y-Velocity", rb.velocity.y);
        }

        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Jumping Controls
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpSFX.Play();
        }

        // Firing Controls
        if (Input.GetButtonDown("Fire1") && Time.time > nextShot)
        {
            StartCoroutine(FireProjectile());
            nextShot = Time.time + fireRate;
        }

        // If the player falls into a pit
        if (transform.position.y < -10f)
        {
            transform.position = startPos;
            hitSound.Play();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy_Platformer"))
                Destroy(enemy);
        }

    }

    // Fire Projectile
    IEnumerator FireProjectile()
    {
        GameObject firedProjectile;
        Vector3 aimVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90f;

        firedProjectile = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
        if (transform.localScale.x == 1f)
        {
            firedProjectile.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            firedProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(projectileForce, 0f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            firedProjectile.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            firedProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(-projectileForce, 0f, 0f), ForceMode2D.Impulse);
        }
        Destroy(firedProjectile, 5f);
        shootSFX.Play();

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy_Platformer")
        {
            jumpSFX.Play();
        }
    }

    public void changeHealth(int x)
    {
        if (x < 0)
        {
            hitSound.Play();
        }

        currentHealth += x;

        if (currentHealth <= 0)
        {
            transform.position = startPos;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy_Platformer"))
                Destroy(enemy);
            currentHealth = maxHealth;
        }

        GameObject.Find("Health Bar").GetComponent<Slider>().value = currentHealth;

    }
}
