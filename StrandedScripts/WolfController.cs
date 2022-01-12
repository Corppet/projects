using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class WolfController : MonoBehaviour
{
    // Patrolling
    private Vector3 walkPoint;
    [SerializeField] private float walkPointRange = 30f;
    [SerializeField] private float standDuration = 3f;
    private bool walkPointSet;

    // Attacking
    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private float knockbackForce = 10f;

    // States
    [SerializeField] private float sightRange = 30f;
    private bool playerInSight;

    [Space]

    // Other Serialized Fields
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private string bulletTag;
    [SerializeField] private string playerTag;
    [SerializeField] private AudioSource detectSound;
    [SerializeField] private AudioSource attackSound;

    // Other Variables
    public bool isLockedOn; // is a bullet locked onto this target?

    private Transform player;
    private bool detectSoundPlayed;

    private void Start()
    {
        player = GameObject.Find("Player Object").transform;
        isLockedOn = false;
    }

    private void Patrolling()
    {
        // Destination Check
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        else
            SearchWalkPoint();

        // Distance to destination
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 5f)
            StartCoroutine(StandStill(standDuration));
    }

    private IEnumerator StandStill(float duration)
    {
        yield return new WaitForSeconds(duration);

        walkPointSet = false;
    }

    // Find a random position to patrol towards
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;
    }

    // Follow the player
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // Knock the wolf back on hit
    private IEnumerator Knockback(Vector3 direction)
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(attackSpeed);

        agent.isStopped = false;
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (playerInSight)
        {
            if (!detectSoundPlayed)
            {
                detectSound.Play();
                detectSoundPlayed = true;
            }
            ChasePlayer();
        }
        else
        {
            Patrolling();
            detectSoundPlayed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(bulletTag))
        {
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag(playerTag))
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.y += knockbackForce * .01f;
            attackSound.Play();
            StartCoroutine(Knockback(direction));
        }
    }
}