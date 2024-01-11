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
        _isGrounded = skeleton_Melee.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton_Melee.SetVelocityX(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            Debug.Log("Grounded");
            skeleton_Melee.StartCoroutine(skeleton_Melee.DestroyObject());
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
