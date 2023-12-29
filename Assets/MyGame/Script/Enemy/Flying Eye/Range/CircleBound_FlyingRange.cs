using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBound_FlyingRange : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private FlyingEye_Range flyingEye_range;

    private void Awake()
    {
        playerObj = GameObject.Find("BonzePlayer");
        flyingEye_range = transform.GetComponentInParent<FlyingEye_Range>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerObj)
        {
            flyingEye_range.SetBound(true);
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
            flyingEye_range.SetBound(false);
        }
    }
}
