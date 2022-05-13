using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    public Transform MainCamTransform;

    // Start is called before the first frame update
    void Start()
    {
        MainCamTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(90f, MainCamTransform.eulerAngles.y, 0f);//follow camera rotation

    }
}
