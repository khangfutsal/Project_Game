using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_DeathState : EnemyState
{
    private Skeleton_Melee skeleton_Melee;
    private bool _isGrounded;
    public SkeletonMelee_DeathState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeleton_Melee = (Skeleton_Melee)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Melee.colliderEnvironment.GetComponent<BoxCollider2D>().isTrigger = true;

        skeleton_Melee.rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;

        skeleton_Melee.KnockBack(25, 10);

        



    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        skeleton_Melee.StartCoroutine(skeleton_Melee.DestroyObject());

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
