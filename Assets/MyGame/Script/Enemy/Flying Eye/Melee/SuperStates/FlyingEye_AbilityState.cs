using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_AbilityState : EnemyState
{
    protected FlyingEye_Melee flyingEye_Melee;
    private bool _isTakeDmg;
    public FlyingEye_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEye_Melee = (FlyingEye_Melee)enemy;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationTriggerFinished()
    {
        base.AnimationTriggerFinished();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //_isTakeDmg = GameController.GetInstance().player.GetBool_AttackEnemy();
    }

    public override void Enter()
    {
        base.Enter();
        //GameController.GetInstance().player.SetBool_AttackEnemy(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_isTakeDmg)
        {
            stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_TakeDamageState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
