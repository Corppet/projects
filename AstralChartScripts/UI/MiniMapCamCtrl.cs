using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamCtrl : MonoBehaviour
{

    public Transform PlayerTransform;
    public Transform MainCamTransform;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.Find("PlayerHolder").transform.GetChild(0).transform;
        MainCamTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerTransform != null)
        {
            Vector3 newPosition = PlayerTransform.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            //transform.rotation = Quaternion.Euler(90f, PlayerTransform.eulerAngles.y, 0f);//follow player rotation
            transform.rotation = Quaternion.Euler(90f, MainCamTransform.eulerAngles.y, 0f);//follow camera rotation
        }
    }
}
