using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class MMUIController : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera menuCamera;

    private void Start()
    {
        Time.timeScale = 1;
        UpdateCam(menuCamera);
    }

    public void UpdateCam(CinemachineVirtualCamera camera)
    {
        foreach (CinemachineVirtualCamera i in cameras)
        {
            if (i == camera)
                i.Priority = 11;
            else
                i.Priority = 10;
        }    
    }
}
