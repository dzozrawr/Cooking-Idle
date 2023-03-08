using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamerasController : MonoBehaviour
{
    public CinemachineVirtualCamera playerVCam;
    public CinemachineVirtualCamera cuttingBoardVCam;
    public CinemachineVirtualCamera customerVCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerVCam.Priority = 10;
            cuttingBoardVCam.Priority = 5;
            customerVCam.Priority = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerVCam.Priority = 5;
            cuttingBoardVCam.Priority = 10;
            customerVCam.Priority = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerVCam.Priority = 5;
            cuttingBoardVCam.Priority = 5;
            customerVCam.Priority = 10;
        }
    }
}
