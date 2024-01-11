using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_TakeDamageState : EnemyState
{
    private bool _isKnock;
    private bool _isKnockAlready;
    private bool _isDeath;
    private bool _isGrounded;

    private SkeletonMelee_Data skeletonMeleeData;
    private Skeleton_Melee skeletonMelee;
    public SkeletonMelee_TakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeletonMeleeData = (SkeletonMelee_Data)enemyData;
        skeletonMelee = (Skeleton_Melee)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = skeletonMelee.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        skeletonMelee.SetBool_IsTakeDamage(false);
        skeletonMelee.SetBool_IsKnock(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _isKnock = skeletonMelee.GetBool_IsKnock();
        _isKnockAlready = skeletonMelee.GetBool_IsKnockAlready();
        _isDeath = skeletonMelee.GetBool_IsDeath();
        if (_isDeath) stateMachine.ChangeState(skeletonMelee.skeletonMelee_Death);

        if (_isGrounded)
        {
            if (_isKnock)
            {
                if (Time.time >= startTime + skeletonMeleeData.knockDuration)
                {
                    skeletonMelee.SetBool_IsKnock(false);
                    skeletonMelee.SetVelocityX(0);
                }
            }
        }

        if (_isKnockAlready)
        {
            skeletonMelee.SetBool_IsKnockAlready(false);

            skeletonMelee.rgBody2D.constraints = RigidbodyConstraints2D.FreezeRotation
                                                | RigidbodyConstraints2D.FreezePositionX;

            stateMachine.ChangeState(skeletonMelee.skeletonMelee_Idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
