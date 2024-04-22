using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _ins;
    [SerializeField] private CinemachineVirtualCamera curCinemachineVirtual;
    [SerializeField] private float startIntensity;
    [SerializeField] private float startTime;
    [SerializeField] private float timeTotal;

    public static CameraShake GetInstance() => _ins;
    private void Awake()
    {
        _ins = this;
    }
    public void ShakeCamera(float intensity,float frequencyNoise, float time)
    {
        curCinemachineVirtual = CameraFollow.GetInstance().GetCameraVirtual();
        Debug.Log(curCinemachineVirtual);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = curCinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(cinemachineBasicMultiChannelPerlin);
        if (cinemachineBasicMultiChannelPerlin != null)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequencyNoise;
        }
        startTime = time;
        timeTotal = time;
    }

    private void Update()
    {
        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            if (startTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = curCinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (startTime / timeTotal));
            }
        }
    }
}
