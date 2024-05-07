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
            VFX_Controller.GetInstance().SpawnVFX(hitVFX, collision.transform,"HitVFX");

            IDmgable damageable = collision.GetComponent<IDmgable>();
            Player player = transform.GetComponentInParent<Player>();

            float dmg = player.playerStats.GetInt_AttackDmg();
            if (damageable != null)
            {
                var aSrc = AudioController.GetInstance().manager.GetAudioSource();
                var aClipAttack = AudioController.GetInstance().manager.GetAudioAttack();
                AudioController.GetInstance().StartMusic(aClipAttack, aSrc);

                CameraShake.GetInstance().ShakeCamera(3, .5f,.1f);
                damageable.TakeDamage(dmg, transform);
            }
        }


    }


}
