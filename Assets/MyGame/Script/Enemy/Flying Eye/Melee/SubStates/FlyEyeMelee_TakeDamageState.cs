using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMelee_TakeDamageState : EnemyState
{
    private FlyingEye_Melee flyingEye_Melee;
    private FlyingEyeData flyingEyeData;

    private int dmg;
    private bool _isKnock;
    private bool _isKnockAlready;
    private bool _isDeath;
    public FlyEyeMelee_TakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEye_Melee = (FlyingEye_Melee)enemy;
        flyingEyeData = (FlyingEyeData)enemyData;
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

        //dmg = GameController.GetInstance().player.GetInt_AttackDmg();
        //dmg = flyingEye_Melee.playerTf.GetComponent<Player>().GetInt_AttackDmg();
        //flyingEye_Melee.TakeDamage(dmg);
        flyingEye_Melee.SetBool_IsKnock(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _isKnock = flyingEye_Melee.GetBool_IsKnock();
        _isKnockAlready = flyingEye_Melee.GetBool_IsKnockAlready();
        _isDeath = flyingEye_Melee.GetBool_IsDeath();
        if (_isDeath) stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_DeathState);

        if (_isKnock)
        {
            if (Time.time >= startTime + flyingEyeData.knockDuration)
            {
                flyingEye_Melee.SetBool_IsKnock(false);
                flyingEye_Melee.SetVelocityX(0);
            }
        }

        if (_isKnockAlready)
        {
            flyingEye_Melee.SetBool_IsKnockAlready(false);
            flyingEye_Melee.rgBody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            stateMachine.ChangeState(flyingEye_Melee.flyEyeMelee_MoveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
