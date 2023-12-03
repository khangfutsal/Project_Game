using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBound_FlyingEYe : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private FlyingEye flyingEye;

    private void Awake()
    {
        playerObj = GameObject.Find("BonzePlayer");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == playerObj)
        {
            flyingEye.SetBound(true);
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
            flyingEye.SetBound(false);
        }
    }
}
