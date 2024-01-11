using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_ReviveState : EnemyState
{
    private Skeleton_Melee skeleton_Melee;
    public SkeletonMelee_ReviveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            
            skeleton_Melee.StartCoroutine(skeleton_Melee.DelayToIdleState());
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
