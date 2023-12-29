using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBound_FlyingEYe : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private FlyingEye_Melee flyingEye_Melee;

    private void Awake()
    {
        playerObj = GameObject.Find("BonzePlayer");
        flyingEye_Melee = transform.GetComponentInParent<FlyingEye_Melee>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == playerObj)
        {
            flyingEye_Melee.SetBound(true);
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
            flyingEye_Melee.SetBound(false);
        }
    }
}
