using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_ReviveState : EnemyState
{
    private Skeleton_Range skeleton_Range;
    public SkeletonRange_ReviveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeleton_Range = (Skeleton_Range)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            skeleton_Range.StartCoroutine(skeleton_Range.DelayToIdleState());
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
