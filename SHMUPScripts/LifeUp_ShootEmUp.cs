using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp_ShootEmUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player_ShootEmUp")
            Destroy(gameObject);
    }
}
