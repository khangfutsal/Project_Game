using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGem : MonoBehaviour
{
    [SerializeField] private static int coin;
    [SerializeField] private Transform playerTf;
    

    [SerializeField] private Rigidbody2D rgbody2D;

    [Header("Properties")]
    [SerializeField] private float speedRotation;

    [SerializeField] private float speedToTarget;
    [SerializeField] private float delay;
    [SerializeField] private float curTime;


    #region Get Function
    public int GetCoin() => coin;
    #endregion

    #region Set Function
    public void SetCoin(int _coin) => coin = _coin;
    #endregion

    private void Awake()
    {
        rgbody2D = GetComponent<Rigidbody2D>();
        playerTf = GameObject.FindObjectOfType<Player>().transform;
    }

    private void Start()
    {
        curTime = Time.time;
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
            ++coin;
            CollectionUI.GetInstance().ShowGroupCoinUI();
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
