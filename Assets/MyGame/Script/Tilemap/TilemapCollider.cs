using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapCollider : MonoBehaviour
{
    [SerializeField] private float dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDmgable Idmg = collision.GetComponent<IDmgable>();
            if(Idmg != null)
            {
                Idmg.TakeDamage(dmg);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDmgable Idmg = collision.GetComponent<IDmgable>();
            if (Idmg != null)
            {
                Idmg.TakeDamage(dmg);
            }
        }
    }
}
