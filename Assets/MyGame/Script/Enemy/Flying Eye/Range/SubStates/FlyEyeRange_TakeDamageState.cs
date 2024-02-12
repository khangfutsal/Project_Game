using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_TakeDamageState : EnemyState
{
    private FlyingEye_Range flyingEye_Range;
    private FlyingEyeRange_Data flyingEyeData;

    private int dmg;
    private bool _isKnock;
    private bool _isKnockAlready;
    private bool _isDeath;
    public FlyEyeRange_TakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        flyingEye_Range = (FlyingEye_Range)enemy;
        flyingEyeData = (FlyingEyeRange_Data)enemyData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        flyingEye_Range.StopFireBullet();
        flyingEye_Range.SetBool_IsKnock(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _isKnock = flyingEye_Range.GetBool_IsKnock();
        _isKnockAlready = flyingEye_Range.GetBool_IsKnockAlready();
        _isDeath = flyingEye_Range.GetBool_IsDeath();
        if (_isDeath) stateMachine.ChangeState(flyingEye_Range.flyEyeRange_DeathState);

        if (_isKnock)
        {
            if (Time.time >= startTime + flyingEyeData.knockDuration)
            {
                

                flyingEye_Range.SetBool_IsKnock(false);
                flyingEye_Range.SetVelocityX(0);
            }
        }

        if (_isKnockAlready)
        {
            flyingEye_Range.SetBool_IsKnockAlready(false);
            flyingEye_Range.rgBody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            stateMachine.ChangeState(flyingEye_Range.flyEyeRange_MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
