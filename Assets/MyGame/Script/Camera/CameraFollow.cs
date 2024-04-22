using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow _ins;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera currentVirtualCamera;

    public static CameraFollow GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cinemachineBrain = GetComponent<CinemachineBrain>();
            if (cinemachineBrain != null)
            {
                currentVirtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;

                if (currentVirtualCamera != null)
                {
                    Debug.Log("Current virtual camera: " + currentVirtualCamera.Name);
                }
                else
                {
                    Debug.LogWarning("No virtual camera is currently active.");
                }
            }
        }
    }

    public CinemachineVirtualCamera GetCameraVirtual()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            currentVirtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;

            if (currentVirtualCamera != null)
            {
                Debug.Log("Virtual ");
                return currentVirtualCamera;
            }
            else
            {
                Debug.LogWarning("No virtual camera is currently active.");
                return null;
            }
        }
        return null;

    }
}
