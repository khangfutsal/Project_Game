using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCollision : MonoBehaviour
{
    public float damage;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            IDmgable Idmg = other.GetComponent<IDmgable>();
            if (Idmg != null)
            {
                Debug.Log("Hit");
                Idmg.TakeDamage(damage, transform);
            }
        }
    }


}
