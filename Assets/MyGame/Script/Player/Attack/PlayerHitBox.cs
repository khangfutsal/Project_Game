using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {

            FlyingEye_Melee flyingeyeMelee = collision.gameObject.GetComponent<FlyingEye_Melee>();
            bool _isDeath = flyingeyeMelee.GetBool_IsDeath(); if (_isDeath) return;

            flyingeyeMelee.SetBool_IsTakeDamage(true);

            Player player = transform.GetComponentInParent<Player>();
            bool attackSecondAlready = player.GetBool_IsHitAttackSecond();
            bool attackFinalAlready = player.GetBool_IsHitAttackFinal();
            
      
            if (attackSecondAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_9")
            {
                flyingeyeMelee.KnockBack(3, 0);
                return;
            }
            if (attackFinalAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_18")
            {
                flyingeyeMelee.rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                flyingeyeMelee.KnockBack(0, 5);
                StartCoroutine(AttackTimeScale());
                return;
            }
            flyingeyeMelee.KnockBack(.5f, 0);

        }


    }
    public IEnumerator AttackTimeScale()
    {
        float timeDelta = 0.1f;

        while (timeDelta <= 1)
        {


            Time.timeScale = timeDelta;
            timeDelta += Time.deltaTime * 4f;

            yield return null;
        }

        Time.timeScale = 1;


    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        FlyingEye_Melee flyingeyeMelee = collision.gameObject.GetComponent<FlyingEye_Melee>();
    //        flyingeyeMelee.SetBool_IsTakeDamage(false);
    //    }
    //}

}
