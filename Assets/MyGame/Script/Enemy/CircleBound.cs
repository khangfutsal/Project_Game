using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBound : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Transform enemyTf;
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        playerObj = GameObject.Find("BonzePlayer");
        enemy = enemyTf.transform.GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerObj)
        {
            enemy.SetBound(true);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("2 " + collision.gameObject.name);
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerObj)
        {
            enemy.SetBound(false);
        }
    }
}
