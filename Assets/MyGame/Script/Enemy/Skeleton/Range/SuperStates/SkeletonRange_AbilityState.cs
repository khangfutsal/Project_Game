using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_AbilityState : EnemyState
{
    protected Skeleton_Range skeleton_Range;
    private bool _isTakeDmg;
    public SkeletonRange_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
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
        _isTakeDmg = skeleton_Range.GetBool_IsTakeDamage();
        if (_isTakeDmg)
        {
            stateMachine.ChangeState(skeleton_Range.skeletonRange_TakeDamage);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
