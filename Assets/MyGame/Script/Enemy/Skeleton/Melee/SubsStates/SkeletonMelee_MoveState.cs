using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_MoveState : SkeletonMelee_AbilityState
{
    private bool canAttack;
    public SkeletonMelee_MoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        canAttack = skeleton_Melee.CanAttack();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (canAttack)
        {
            stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Attack);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        skeleton_Melee.MoveToPlayer();
    }
}
