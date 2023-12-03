using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            GameController.GetInstance().player.SetBool_AttackEnemy(true);
            Debug.Log("Hit enemy Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameController.GetInstance().player.SetBool_AttackEnemy(false);
        }
    }

}
