using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Platformer : MonoBehaviour
{
    // Adjustable Fields
    [SerializeField] float parallaxFactor;

    [Space]

    // Other Serialized Fields
    [SerializeField] GameObject camera;

    // Other Fields
    float length; // width of image
    float startPos; // starting position
    float newPos; // new position

    // Start is called before the first frame update
    void Start()
    {
        // Initializing Variables
        startPos = transform.position.x;
        newPos = startPos;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame update
    void Update()
    {
        // Parallax Effect
        float distance = (camera.transform.position.x * parallaxFactor);
        transform.position = new Vector3(newPos + distance, transform.position.y, transform.position.z);
        
        float boundary = camera.transform.position.x * (1f - parallaxFactor);
        if (boundary > newPos + length)
        {
            newPos += length;
        }
        else if (boundary <  newPos - length)
        {
            newPos -= length;
        }

        // Reset Position
        if (camera.transform.position.x == -4.5f)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
            newPos = startPos;
        }
    }
}
