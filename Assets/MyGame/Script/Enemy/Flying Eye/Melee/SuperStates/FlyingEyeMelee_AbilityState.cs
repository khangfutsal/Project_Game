using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeMelee_AbilityState : EnemyState
{
    protected FlyingEye_Melee flyingEye_Melee;
    private bool _isTakeDmg;
    public FlyingEyeMelee_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
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
        _isTakeDmg = flyingEye_Melee.GetBool_IsTakeDamage();
    }

    public override void Enter()
    {
        base.Enter();
        //GameController.GetInstance().player.SetBool_AttackEnemy(false);
        //flyingEye_Melee.playerTf.GetComponent<Player>().SetBool_AttackEnemy(false);
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
            flyingEye_Melee.SetBool_IsTakeDamage(false);
            stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_TakeDamageState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
