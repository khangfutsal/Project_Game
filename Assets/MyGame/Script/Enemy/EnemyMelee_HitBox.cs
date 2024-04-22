using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_HitBox : MonoBehaviour
{
    [SerializeField] private Enemy enemy;


    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float dmg = 0;
            enemy = transform.parent.GetComponent<Enemy>();
            if (enemy is FlyingEye_Melee flyingEye)
            {
                dmg = flyingEye.GetFloat_DmgAttack();
            }
            else if (enemy is Skeleton_Melee skeleton)
            {
                dmg = skeleton.GetFloat_DmgAttack();
            }
            //Debug.Log("type : " + enemy.GetType());
            IDmgable damageable = collision.GetComponent<IDmgable>();
            
            if (damageable != null)
            {
                damageable.TakeDamage(dmg, transform);
            }
        }
    }


}
