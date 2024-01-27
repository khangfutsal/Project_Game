using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMelee_DeathState : EnemyState
{
    FlyingEye_Melee flyingEyeMelee;
    private bool isGrounded;
    public FlyEyeMelee_DeathState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEyeMelee = (FlyingEye_Melee)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = flyingEyeMelee.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Death");
        flyingEyeMelee.rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        flyingEyeMelee.KnockBack(25, 10);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        flyingEyeMelee.StartCoroutine(flyingEyeMelee.DestroyObject());


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
