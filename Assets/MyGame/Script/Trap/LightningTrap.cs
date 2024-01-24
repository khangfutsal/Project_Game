using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        curTime = Time.time;
    }

    private void Update()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        if (Time.time >= (curTime + timeDelay))
        {
            anim.SetBool("Attack", true);


        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                curTime = Time.time;
                anim.SetBool("Attack", false);
            }
        }
    }

}
