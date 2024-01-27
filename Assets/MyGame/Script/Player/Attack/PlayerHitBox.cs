using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject hitVFX = VFX_Controller.GetInstance().GetVFX_Manager().GetHitVFX();
            VFX_Controller.GetInstance().SpawnVFX(hitVFX, collision.transform);

            IDmgable damageable = collision.GetComponent<IDmgable>();
            Player player = transform.GetComponentInParent<Player>();

            float dmg = player.GetInt_AttackDmg();
            if (damageable != null)
            {
                Debug.Log("1");
                damageable.TakeDamage(dmg, transform);
            }



        }


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
