using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    // Adjustable Values
    [SerializeField] private float spotRange = 30f; // range to lock on enemies
    [SerializeField] private float speed = 10f;

    // Other Serialized Fields
    [SerializeField] private string enemy1Tag;
    [SerializeField] private string enemy2Tag;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioSource bulletSound;

    // Other Variables
    private bool targetFound;
    private GameObject targetObject;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        // Find a target
        if (!findTarget())
            Destroy(gameObject);
        bulletSound.Play();
    }

    private bool findTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spotRange, enemyLayer);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag(enemy1Tag) && !hitColliders[i].GetComponent<SnakeController>().isLockedOn)
            {
                hitColliders[i].GetComponent<SnakeController>().isLockedOn = true;
                targetObject = hitColliders[i].gameObject;
                return true;
            }
            else if (hitColliders[i].CompareTag(enemy2Tag) && !hitColliders[i].GetComponent<WolfController>().isLockedOn)
            {
                hitColliders[i].GetComponent<WolfController>().isLockedOn = true;
                targetObject = hitColliders[i].gameObject;
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, speed * Time.deltaTime);
        }
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetObject)
        {
            Destroy(gameObject);
        }
    }
}
