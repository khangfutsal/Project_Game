using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee_AbilityState : EnemyState
{
    protected Skeleton_Melee skeleton_Melee;

    private bool _isTakeDmg;
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
        _isTakeDmg = skeleton_Melee.GetBool_IsTakeDamage();
        if (_isTakeDmg)
        {
            stateMachine.ChangeState(skeleton_Melee.skeletonMelee_TakeDamage);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
