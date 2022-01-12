using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] GameObject hitEffect; // explosion animation

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = Instantiate(hitEffect, transform.position, Quaternion.identity);
        hitObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Destroy(hitObject, .5f);
        Destroy(gameObject);
    }
}
