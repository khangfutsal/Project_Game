using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_AttackState : SkeletonRange_AbilityState
{
    private bool _canAttack;
    public SkeletonRange_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Range.skeletonRange_Data.curTimeAttack = Time.time;
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
            stateMachine.ChangeState(skeleton_Range.skeletonRange_Idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
