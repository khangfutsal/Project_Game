using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_MoveState : SkeletonRange_AbilityState
{
    private bool canAttack;
    private float timeAttack;
    private float curTimeAttack;
    public SkeletonRange_MoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        canAttack = skeleton_Range.CanAttack();
    }

    public override void Enter()
    {
        base.Enter();
        timeAttack = skeleton_Range.skeletonRange_Data.timeAttack;
        curTimeAttack = skeleton_Range.skeletonRange_Data.curTimeAttack;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (canAttack && Time.time >= curTimeAttack + timeAttack) stateMachine.ChangeState(skeleton_Range.skeletonRange_Attack);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        skeleton_Range.Chase();
    }
}
