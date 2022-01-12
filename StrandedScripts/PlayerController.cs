using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    // Adjustable Values
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float throwRate = 1f;
    [SerializeField] private float projectileForce = 25f;
    [SerializeField] private float throwForce = 25f;
    [SerializeField] private float gravityValue = -49.05f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int rangedDmgReceived = 15; // damage taken from a ranged attack
    [SerializeField] private int meleeDmgReceived = 20; // damage taken from a melee attack
    [SerializeField] private float hitBuffer = 1f; // invulnerabity after taking a hit
    [SerializeField] private float timeLimit = 180f; // game timer

    [Space]

    // Other Serialized Fields
    [SerializeField] private string venomTag;
    [SerializeField] private string wolfTag;
    [SerializeField] private GameObject dividerProjectile;
    [SerializeField] private GameObject ammoBox;
    [SerializeField] private Transform pickupPoint;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private Canvas HUD;

    // Other Variables
    private int health;
    private bool nextShot; // is the next shot ready
    private bool nextThrow;
    // private int ammoBoxCount; // number of ammo boxes in inventory
    private bool invulerable;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction pickupAction;
    private InputAction throwAction;
    private InputAction exitAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        pickupAction = playerInput.actions["Pickup"];
        throwAction = playerInput.actions["Throw"];
        exitAction = playerInput.actions["Exit"];
    }

    private void Start()
    {
        health = startingHealth;
        HUD.GetComponent<HUDController>().SetHealth(health);
        StartCoroutine(HUD.GetComponent<HUDController>().StartTimer(timeLimit));
        nextShot = true;
        nextThrow = true;
        // ammoBoxCount = 0;
        invulerable = false;
    }

    private IEnumerator ShootDivider()
    {
        nextShot = false;

        GameObject splitBall = GameObject.Instantiate(dividerProjectile, cameraTransform.position, Quaternion.identity);
        splitBall.GetComponent<Rigidbody>().AddForce(cameraTransform.forward * projectileForce, ForceMode.Impulse);
        shootSound.Play();

        yield return new WaitForSeconds(fireRate);

        nextShot = true;
    }

    private IEnumerator ThrowAmmo()
    {
        nextThrow = false;

        GameObject ammoPrefab = GameObject.Instantiate(ammoBox, pickupPoint.position, Quaternion.identity);
        Vector3 direction = cameraTransform.forward;
        direction.y += throwForce * .01f;
        ammoPrefab.GetComponent<Rigidbody>().AddForce(direction * throwForce, ForceMode.Impulse);
        // ammoBoxCount--;

        yield return new WaitForSeconds(throwRate);

        nextThrow = true;
    }

    private IEnumerator TakeHit(string enemy)
    {
        invulerable = true;

        if (enemy == "Snake")
        {
            health -= rangedDmgReceived;
        }
        else if (enemy == "Wolf")
        {
            health -= meleeDmgReceived;
        }

        if (health <= 0)
        {
            TriggerLose();
        }

        HUD.GetComponent<HUDController>().SetHealth(health);

        yield return new WaitForSeconds(hitBuffer);

        invulerable = false;
    }

    public void TriggerLose()
    {
        SceneManager.LoadScene("Lose Scene");
    }

    void Update()
    {
        // Exit Binding
        //if (exitAction.triggered)
        //    SceneManager.LoadScene("Menu Scene");

        // Ground Check
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Player Rotation
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Movement
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Jump
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Shoot
        if (shootAction.triggered && nextShot)
        {
            StartCoroutine(ShootDivider());
        }

        // Throw Ammo Box
        if (throwAction.triggered && nextThrow)
        {
            StartCoroutine(ThrowAmmo());
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(venomTag) && !invulerable)
        {
            StartCoroutine(TakeHit("Snake"));
        }
        else if (collision.transform.CompareTag(wolfTag) && !invulerable)
        {
            StartCoroutine(TakeHit("Wolf"));
        }
    }
}