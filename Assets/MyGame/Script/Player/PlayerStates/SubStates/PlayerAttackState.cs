using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float statusFireball;
    private float statusEarthquake;

    private bool _isHurt;
    private bool earthquakeInput;
    private bool fireballInput;

    public bool attackInput;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void DoChecks()
    {
        base.DoChecks();
        _isHurt = player.GetBool_Hurt();
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);

        statusFireball = player.playerStats.GetFloat_StatusFireBall();
        statusEarthquake = player.playerStats.GetFloat_StatusEarthquake();

        attackInput = player.playerInputHandler.attackInput;
        earthquakeInput = player.playerInputHandler.earthquakeInput;
        fireballInput = player.playerInputHandler.fireBallInput;


        if (_isHurt)
        {
            stateMachine.ChangeState(player.playerTakeDamageState);
        }
        else if (earthquakeInput && statusEarthquake == 1)
        {
            stateMachine.ChangeState(player.playerSkillEarthQuake);
        }
        else if (fireballInput && statusFireball == 1)
        {
            stateMachine.ChangeState(player.playerSkillFireBall);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    
}
