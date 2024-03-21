using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGem : MonoBehaviour
{

    [SerializeField] private Transform playerTf;

    [SerializeField] private Rigidbody2D rgbody2D;
    [SerializeField] private TrailRenderer trail;

    [Header("Properties")]
    [SerializeField] private float speedRotation;

    [SerializeField] private float speedToTarget;
    [SerializeField] private float delay;
    [SerializeField] private float curTime;



    private void Awake()
    {
        rgbody2D = GetComponent<Rigidbody2D>();
        playerTf = GameObject.FindObjectOfType<Player>().transform;
        trail = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        if (!transform.gameObject.activeSelf) return;
        if (Time.time >= curTime + delay)
        {
            rgbody2D.bodyType = RigidbodyType2D.Kinematic;
            Vector3 direction = playerTf.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.up, direction.normalized, speedRotation * Time.deltaTime, 0.0f);
            transform.up = newDirection;
            transform.position = Vector3.MoveTowards(transform.position, playerTf.position, speedToTarget * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(transform.gameObject);

            var crystal = GameController.GetInstance().gameManager.GetCrystal();

            GameController.GetInstance().gameManager.SetCrystalUI(++crystal);
            GameController.GetInstance().gameManager.SetCrystal(++crystal);

            DataManager.GetInstance().dataPlayerSO.curCrystal = crystal;

            CollectionUI.GetInstance().ShowGroupCrystalUI();

        }
    }

    private void OnEnable()
    {
        curTime = Time.time;
        
    }
    private void OnDisable()
    {
        rgbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
