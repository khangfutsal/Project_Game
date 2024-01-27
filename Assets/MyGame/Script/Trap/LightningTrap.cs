using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    private float damage;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        data.curTime = Time.time;
    }

    private void Update()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        if (Time.time >= (data.curTime + data.timeDelay))
        {
            anim.SetBool("Attack", true);


        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                data.curTime = Time.time;
                anim.SetBool("Attack", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDmgable damageable = collision.GetComponent<IDmgable>();
            damage = data.dmg;
            if (damageable != null)
            {
                damageable.TakeDamage(data.dmg);
            }
        }
    }

}
