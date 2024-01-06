using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_DeathState : EnemyState
{
    private FlyingEye_Range flyingEyeRange;
    private bool isGrounded;
    public FlyEyeRange_DeathState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEyeRange = (FlyingEye_Range)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = flyingEyeRange.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Death");
        flyingEyeRange.SetVelocityX(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isGrounded)
        {
            Debug.Log("Grounded");
            flyingEyeRange.StartCoroutine(flyingEyeRange.DestroyObject());
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
