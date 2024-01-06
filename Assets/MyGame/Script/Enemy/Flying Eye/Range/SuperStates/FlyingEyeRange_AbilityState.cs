using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeRange_AbilityState : EnemyState
{
    protected FlyingEye_Range flyingEye_Range;
    private bool _isTakeDmg;
    public FlyingEyeRange_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEye_Range = (FlyingEye_Range)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isTakeDmg = flyingEye_Range.GetBool_IsTakeDamage();
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
        if(_isTakeDmg)
        {
            flyingEye_Range.SetBool_IsTakeDamage(false);
            stateMachine.ChangeState(flyingEye_Range.flyEyeRange_TakeDamageState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
