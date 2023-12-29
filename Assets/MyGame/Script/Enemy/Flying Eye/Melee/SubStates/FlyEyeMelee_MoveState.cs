using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMelee_MoveState : FlyingEyeMelee_AbilityState
{
    private bool isBound;
    private bool canAttack;
    private float timeDelay = 1f;
    public FlyEyeMelee_MoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationTriggerFinished()
    {
        base.AnimationTriggerFinished();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        canAttack = flyingEye_Melee.CanAttack();
        
    }

    public override void Enter()
    {
        base.Enter();
        flyingEye_Melee.rgBody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isBound = flyingEye_Melee.isBound;
        
        if (isBound && Time.time >= startTime + timeDelay)
        {
            flyingEye_Melee.MoveToPlayer();
            if (canAttack)
            {
                stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_AttackState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        flyingEye_Melee.SetDefault_Moving();
    }
}
