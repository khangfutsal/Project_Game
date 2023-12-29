using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeRange_AbilityState : EnemyState
{
    protected FlyingEye_Range flyingEye_Range;
    public FlyingEyeRange_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEye_Range = (FlyingEye_Range)enemy;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
