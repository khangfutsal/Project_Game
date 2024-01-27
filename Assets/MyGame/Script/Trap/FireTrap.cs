using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
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
}
