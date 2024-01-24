using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_IdleState : SkeletonRange_AbilityState
{
    private bool isBound;
    private float timeDelay = 1f;
    public SkeletonRange_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Range.rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        skeleton_Range.transform.tag = "Enemy";
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isBound = skeleton_Range.isBound;
        if (isBound && Time.time >= startTime + timeDelay)
        {
            stateMachine.ChangeState(skeleton_Range.skeletonRange_Move);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
