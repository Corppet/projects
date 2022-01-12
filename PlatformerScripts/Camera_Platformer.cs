using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Platformer : MonoBehaviour
{
    // Adjustable Fields
    [SerializeField] float xBarrier; // maximum horizontal gap between player and camera center
    [SerializeField] float yBarrier; // maximum vertical gap between player and camera center

    [Space]

    // Other Serialized Fields
    [SerializeField] GameObject player;

    // Other Fields
    float moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Lower Barrier
        Debug.DrawLine(new Vector3(transform.position.x - GetComponent<Camera>().orthographicSize * (16f / 9f), transform.position.y - yBarrier, transform.position.z), new Vector3(transform.position.x + GetComponent<Camera>().orthographicSize * (16f / 9f), transform.position.y - yBarrier, transform.position.z), Color.cyan);
        
        // Upper Barrier
        Debug.DrawLine(new Vector3(transform.position.x - GetComponent<Camera>().orthographicSize * (16f / 9f), transform.position.y + yBarrier, transform.position.z), new Vector3(transform.position.x + GetComponent<Camera>().orthographicSize * (16f / 9f), transform.position.y + yBarrier, transform.position.z), Color.cyan);
        
        // Left Barrier
        Debug.DrawLine(new Vector3(transform.position.x - xBarrier, transform.position.y - GetComponent<Camera>().orthographicSize, transform.position.z), new Vector3(transform.position.x - xBarrier, transform.position.y + GetComponent<Camera>().orthographicSize, transform.position.z), Color.blue);

        // Right Barrier
        Debug.DrawLine(new Vector3(transform.position.x + xBarrier, transform.position.y - GetComponent<Camera>().orthographicSize, transform.position.z), new Vector3(transform.position.x + xBarrier, transform.position.y + GetComponent<Camera>().orthographicSize, transform.position.z), Color.blue);

        // Following Player Horizontal Movement
        if (player.transform.position.x - transform.position.x > xBarrier)
        {
            transform.position = new Vector3(player.transform.position.x - xBarrier, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x - transform.position.x < -xBarrier && transform.position.x > -4.5f)
        {
            transform.position = new Vector3(player.transform.position.x + xBarrier, transform.position.y, transform.position.z);
        }

        // Following Player Vertical Movement
        if (player.transform.position.y - transform.position.y > yBarrier)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - yBarrier, transform.position.z);
        }
        else if (player.transform.position.y - transform.position.y < -yBarrier && transform.position.y > -2.5f)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + yBarrier, transform.position.z);
        }

        // Camera Boundaries
        if (transform.position.x < -4.5f)
        {
            transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
        }
        
        if (transform.position.y < -2.5f)
        {
            transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);
        }
    }
}
