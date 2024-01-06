using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_AttackState : SkeletonMelee_AbilityState
{
    private bool canAttack;
    private bool _isBound;
    private Skeleton_Melee skeletonMelee;
    public SkeletonMelee_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        skeletonMelee = (Skeleton_Melee)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        canAttack = skeleton_Melee.CanAttack();

        _isBound = skeletonMelee.isBound;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!_isBound) stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Idle);
        if (!canAttack) stateMachine.ChangeState(skeleton_Melee.skeletonMelee_Idle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
