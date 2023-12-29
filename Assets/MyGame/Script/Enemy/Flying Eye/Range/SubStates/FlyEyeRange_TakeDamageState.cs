using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_TakeDamageState : FlyingEyeRange_AbilityState
{
    public FlyEyeRange_TakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
