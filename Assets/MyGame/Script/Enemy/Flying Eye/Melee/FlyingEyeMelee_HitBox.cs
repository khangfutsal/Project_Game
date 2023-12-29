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
            Player player = collision.gameObject.GetComponent<Player>();
            FlyingEye_Melee flyingEye = transform.GetComponentInParent<FlyingEye_Melee>();

            bool defenseInput = player.playerInputHandler.defenseInput;

            int playerFaceDirection = player.facingDirection;
            int enemyFaceDirection = flyingEye.facingDirection;

            float enemyDamage = flyingEye.GetFloat_DmgAttack();
            float totalDamage;
            if (defenseInput)
            {
                if (playerFaceDirection != enemyFaceDirection)
                {
                    totalDamage = enemyDamage * .8f;
                    Debug.Log(totalDamage);
                    player.SetBool_IsHurt(true);
                    player.TakeDamage(totalDamage);
                    return;
                }
                else
                {
                    totalDamage = enemyDamage;
                    player.SetBool_IsHurt(true);
                    player.TakeDamage(totalDamage);
                    
                    player.playerStateMachine.ChangeState(player.playerTakeDamageState);
                    return;
                }
            }
            totalDamage = enemyDamage;
            player.SetBool_IsHurt(true);
            player.TakeDamage(totalDamage);

            Debug.Log("Hit Player Enter : " + flyingEye.gameObject.name);
        }
    }


}
