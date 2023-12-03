using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMelee_TakeDamageState : EnemyState
{
    private int dmg;
    private FlyingEye_Melee flyingEye_Melee;
    public FlyEyeMelee_TakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
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
    }

    public override void Enter()
    {
        base.Enter();
        
        dmg = GameController.GetInstance().player.GetInt_AttackDmg();
        flyingEye_Melee.TakeDamage(dmg);
        GameController.GetInstance().player.SetBool_AttackEnemy(false);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_MoveState);
        }

        if(GameController.GetInstance().player.GetBool_AttackEnemy())
        {
            stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
