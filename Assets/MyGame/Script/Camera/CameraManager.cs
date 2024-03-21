using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   [SerializeField] private List<CinemachineVirtualCamera> cameras;

    public List<CinemachineVirtualCamera> GetCameras() => cameras;
}
