using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_MoveState : FlyingEyeRange_AbilityState
{
    private bool isBound;
    private bool canAttack;
    private float timeDelay = 1f;
    public FlyEyeRange_MoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        flyingEye_Range.rgBody2D.constraints = RigidbodyConstraints2D.FreezePositionY 
            | RigidbodyConstraints2D.FreezeRotation 
            | RigidbodyConstraints2D.FreezePositionX;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isBound = flyingEye_Range.isBound;
        canAttack = flyingEye_Range.CanAttack();

        if (isBound && Time.time >= startTime + timeDelay)
        {
            flyingEye_Range.Chase();
            if (canAttack)
            {
                stateMachine.ChangeState(flyingEye_Range.flyEyeRange_AttackState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
