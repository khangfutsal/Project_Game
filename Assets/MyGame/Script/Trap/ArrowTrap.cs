using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : Trap
{
    private Object_Pool objPool;
    [SerializeField] private Transform pointAttack;
    private void Awake()
    {
        objPool = GetComponent<Object_Pool>();
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

    public void SpawnArrow()
    {
        ArrowBullet bullet = objPool.GetTransformFromPool().GetComponent<ArrowBullet>();
        bullet.SetDirection(pointAttack);
        bullet.gameObject.SetActive(true);
    }
}
