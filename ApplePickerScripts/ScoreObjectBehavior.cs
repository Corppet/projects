using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObjectBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AppleTreeManager").GetComponent<AppleTree>().AddItem(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // When the object leaves the screen
        if (gameObject.transform.position.y < -5f)
        {
            GameObject.Find("AppleTreeManager").GetComponent<AppleTree>().DecreaseScore(3);
            GameObject.Find("Player").GetComponent<PlayerBehaviour>().Downgrade();
            foreach (GameObject potion in GameObject.FindGameObjectsWithTag("Potion"))
            {
                Destroy(potion);
                GameObject.Find("AppleTreeManager").GetComponent<AppleTree>().DeleteItem(potion);
            }

        }
    }

    // When the object collides with the catcher
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("AppleTreeManager").GetComponent<AppleTree>().IncreaseScore(1);
        Destroy(gameObject);
        GameObject.Find("AppleTreeManager").GetComponent<AppleTree>().DeleteItem(gameObject);
    }
}
