using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObjectBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // When the object leaves the screen
        if (gameObject.transform.position.y < -5f)
            Destroy(gameObject);
    }

    // When the object collides with the catcher
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().Upgrade();
        Destroy(gameObject);
    }
}
