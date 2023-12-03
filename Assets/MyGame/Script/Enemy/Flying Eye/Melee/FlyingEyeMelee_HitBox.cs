using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeMelee_HitBox : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.GetInstance().player.SetBool_Hurt(true);
            Debug.Log("Hit Player Enter");
        }
    }


}
