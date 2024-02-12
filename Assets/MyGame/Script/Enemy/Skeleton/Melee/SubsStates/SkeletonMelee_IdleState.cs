using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_IdleState : SkeletonMelee_AbilityState
{
    private bool isBound;
    private float timeDelay = 1f;
    public SkeletonMelee_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Melee.rgBody2D.constraints &= ~(RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX);

        skeleton_Melee.transform.tag = "Enemy";
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isBound = skeleton_Melee.isBound;
        if (isBound)
        {
            stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Move);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
