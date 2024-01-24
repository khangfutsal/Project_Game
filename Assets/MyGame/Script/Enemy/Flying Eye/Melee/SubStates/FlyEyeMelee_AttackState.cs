using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMelee_AttackState : FlyingEyeMelee_AbilityState
{

    public FlyEyeMelee_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
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
        if (isAnimationFinished) stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_MoveState);
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
