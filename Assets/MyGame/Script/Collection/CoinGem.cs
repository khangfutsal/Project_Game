using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGem : MonoBehaviour
{
    [SerializeField] private Transform playerTf;

    [Header("Properties")]
    [SerializeField] private float speedRotation;

    [SerializeField] private float speedToTarget;


    private void FixedUpdate()
    {
        if (!transform.gameObject.activeSelf) return;
        Vector3 direction = playerTf.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.up, direction.normalized, speedRotation * Time.deltaTime, 0.0f);
        transform.up = newDirection;
        transform.position = Vector3.MoveTowards(transform.position, playerTf.position, speedToTarget * Time.deltaTime);

    }

    private void OnEnable()
    {

    }
}
