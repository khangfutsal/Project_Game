using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_AttackState : FlyingEyeRange_AbilityState
{
    private bool _canAttack;
    public FlyEyeRange_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _canAttack = flyingEye_Range.GetBool_IsFired();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit");
        flyingEye_Range.anim.SetBool("fire", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_canAttack)
        {
            flyingEye_Range.SetBool_IsFired(false);
            flyingEye_Range.anim.SetBool("fire", true);
            stateMachine.ChangeState(flyingEye_Range.flyEyeRange_MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
