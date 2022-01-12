using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SnakeController : MonoBehaviour
{
    // Patrolling
    private Vector3 walkPoint;
    [SerializeField] private float walkPointRange = 30f;
    [SerializeField] private float standDuration = 3f;
    private bool walkPointSet;

    // Attacking
    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private float venomForce = 25f;
    private bool alreadyAttacked;
    private bool detectSoundPlayed;

    // States
    [SerializeField] private float sightRange = 30f;
    [SerializeField] private float attackRange = 20f;
    private bool playerInSight, playerInAttack;

    [Space]

    // Other Serialized Fields
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private GameObject venomPrefab;
    [SerializeField] private Transform mouthTransform;
    [SerializeField] private string bulletTag;
    [SerializeField] private AudioSource detectSound;

    // Other Variables
    public bool isLockedOn; // is a bullet locked onto this target?

    private Transform player;

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

    // Attack the player
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            GameObject bullet = GameObject.Instantiate(venomPrefab, mouthTransform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * venomForce, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed); // Attack Speed
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (playerInSight)
        {
            if (!detectSoundPlayed)
            {
                detectSound.Play();
                detectSoundPlayed = true;
            }
            if (playerInAttack)
                AttackPlayer();
            else
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
    }
}