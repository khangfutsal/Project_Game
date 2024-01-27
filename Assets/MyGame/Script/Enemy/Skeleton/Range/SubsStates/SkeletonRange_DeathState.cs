using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_DeathState : EnemyState
{
    private Skeleton_Range skeleton_Range;
    private bool _isGrounded;
    public SkeletonRange_DeathState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeleton_Range = (Skeleton_Range)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = skeleton_Range.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Range.colliderEnvironment.GetComponent<BoxCollider2D>().isTrigger = true;

        skeleton_Range.rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        
        skeleton_Range.KnockBack(25, 10);


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        skeleton_Range.StartCoroutine(skeleton_Range.DestroyObject());

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
