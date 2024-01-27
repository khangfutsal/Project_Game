using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_Defense : EnemyState
{
    private float defHoldTime;

    private bool _isTakeDmg;
    private bool _isGrounded;

    private bool _isKnock;
    private bool _isKnockAlready;
    private bool _isDeath;

    private Skeleton_Melee skeleton_Melee;
    private SkeletonMelee_Data skeletonMelee_Data;
    public SkeletonMelee_Defense(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeleton_Melee = (Skeleton_Melee)enemy;
        skeletonMelee_Data = (SkeletonMelee_Data)enemyData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isTakeDmg = skeleton_Melee.GetBool_IsTakeDamage();
        _isGrounded = skeleton_Melee.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Melee.SetBool_IsDefense(true);

        defHoldTime = skeleton_Melee.DefenseHoldTime(3, 5);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton_Melee.anim.SetBool("Def", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_isTakeDmg)
        {
            _isKnock = skeleton_Melee.GetBool_IsKnock();
            _isKnockAlready = skeleton_Melee.GetBool_IsKnockAlready();
            _isDeath = skeleton_Melee.GetBool_IsDeath();
    
            if (_isDeath) stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Death);

            else if (_isGrounded)
            {
                if (_isKnock)
                {
                    if (Time.time >= startTime + skeletonMelee_Data.knockDuration)
                    {
                        Debug.Log("Knock");
                        skeleton_Melee.SetBool_IsKnock(false);
                        skeleton_Melee.SetVelocityX(0);
                    }
                }
            }
            if (_isKnockAlready)
            {
                skeleton_Melee.SetBool_IsKnockAlready(false);

                skeleton_Melee.rgBody2D.constraints = RigidbodyConstraints2D.FreezeRotation
                                                    | RigidbodyConstraints2D.FreezePositionX;
            }

            skeleton_Melee.SetBool_IsTakeDamage(false);
            skeleton_Melee.anim.SetBool("Def", true);
        }
        else skeleton_Melee.anim.SetBool("Def", false);

        if (Time.time >= startTime + defHoldTime)
        {
            skeleton_Melee.SetBool_IsDefense(false);
            stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
