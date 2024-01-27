using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBullet : MonoBehaviour
{

    [SerializeField] private GameObject bulletObj;

    [SerializeField] private float speed;

    private ToxicTrap toxicTrap;
    private float dmg;

    public Rigidbody2D rgbody2D;

    private void Awake()
    {
        toxicTrap = transform.parent.parent.GetComponentInChildren<ToxicTrap>();
        rgbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InActive();
            IDmgable damageable = collision.GetComponent<IDmgable>();
            dmg = toxicTrap.data.dmg;
            if (damageable != null)
            {
                damageable.TakeDamage(dmg);
            }
        }

    }

    private void OnEnable()
    {
        Invoke("InActive", 7f);
    }

    private void InActive()
    {
        transform.gameObject.SetActive(false);
    }


}
