using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_TakeDamageState : EnemyState
{
    private bool _isKnock;
    private bool _isKnockAlready;
    private bool _isDeath;
    private bool _isGrounded;

    private SkeletonRange_Data skeletonRange_Data;
    private Skeleton_Range skeletonRange;
    public SkeletonRange_TakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeletonRange_Data = (SkeletonRange_Data)enemyData;
        skeletonRange = (Skeleton_Range)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = skeletonRange.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter takedamage");
        skeletonRange.SetBool_IsTakeDamage(false);
        skeletonRange.SetBool_IsKnock(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _isKnock = skeletonRange.GetBool_IsKnock();
        _isKnockAlready = skeletonRange.GetBool_IsKnockAlready();
        _isDeath = skeletonRange.GetBool_IsDeath();
        if (_isDeath) stateMachine.ChangeState(skeletonRange.skeletonRange_Death);

        if (_isGrounded)
        {
            if (_isKnock)
            {
                if (Time.time >= startTime + skeletonRange_Data.knockDuration)
                {
                    skeletonRange.SetBool_IsKnock(false);
                    skeletonRange.SetVelocityX(0);
                }
            }
        }

        if (_isKnockAlready)
        {
            skeletonRange.SetBool_IsKnockAlready(false);

            skeletonRange.rgBody2D.constraints = RigidbodyConstraints2D.FreezeRotation
                                                | RigidbodyConstraints2D.FreezePositionX;

            stateMachine.ChangeState(skeletonRange.skeletonRange_Idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
