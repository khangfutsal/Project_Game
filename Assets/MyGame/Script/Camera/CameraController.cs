using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraManager manager;

    private static CameraController _ins;

    public static CameraController GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        ShowCamera("CameraPlayer");
    }

    public void ShowCamera(string name)
    {
        var cameras = manager.GetCameras();
        foreach (var camera in cameras)
        {
            camera.gameObject.SetActive(false);

            if(name == camera.name)
            {
                camera.gameObject.SetActive(true);
            }
        }
    }
}
