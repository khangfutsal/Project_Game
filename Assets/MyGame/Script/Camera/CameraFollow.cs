using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTf;

    private Vector3 velocity;

    [SerializeField] private Vector3 posOffset;
    [Range(0, 1)]
    [SerializeField] float smoothTime;
    private void Start()
    {
        velocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        Vector3 playerPos = new Vector3(playerTf.transform.position.x + posOffset.x, playerTf.transform.position.y + posOffset.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);
    }

}
