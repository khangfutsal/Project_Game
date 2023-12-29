using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_AttackState : FlyingEyeRange_AbilityState
{
    
    public FlyEyeRange_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        flyingEye_Range.FireBullet();
        Debug.Log("Attack State Range");
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
