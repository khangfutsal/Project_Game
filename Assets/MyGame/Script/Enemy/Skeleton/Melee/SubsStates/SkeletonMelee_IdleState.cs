using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_IdleState : SkeletonMelee_AbilityState
{
    private bool isBound;
    private bool canAttack;
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isBound = skeleton_Melee.isBound;

        if (isBound && Time.time >= startTime + timeDelay)
        {
            stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Move);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
