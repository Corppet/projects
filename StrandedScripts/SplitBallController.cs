using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBallController : MonoBehaviour
{
    // Adjustable Values
    [SerializeField] private float lifetime = 3f;

    // Other Variables
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Player"))
            Destroy(gameObject);
    }
}
