using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_AbilityState : EnemyState
{
    protected Skeleton_Melee skeleton_Melee;
    public SkeletonMelee_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeleton_Melee = (Skeleton_Melee)enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
