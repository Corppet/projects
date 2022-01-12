using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    // Adjustable Values
    [SerializeField] private int bulletSpawnCount = 4; // number of bullets it will create

    // Serialized Fields
    [SerializeField] private GameObject bulletPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Divider Projectile"))
        {
            for (int i = 0; i < bulletSpawnCount; i++)
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
