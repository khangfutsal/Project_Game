using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;

    protected bool jumpInput;
    private bool meditateInput;
    private bool attackInput;
    private bool defenseInput;
    private bool earthquakeInput;
    private bool fireballInput;

    private float statusDefense;
    private float statusFireball;
    private float statusEarthquake;

    protected bool _isHurt;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isHurt = player.GetBool_Hurt();

        player.CheckInSlope();

    }

    public override void Enter()
    {
        base.Enter();
        player.playerJumpState.ResetAmountOfJumpsLeft();
        player.playerInputHandler.UseAttackInput();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        statusDefense = player.playerStats.GetFloat_StatusDefense();
        statusFireball = player.playerStats.GetFloat_StatusFireBall();
        statusEarthquake = player.playerStats.GetFloat_StatusEarthquake();

        input = player.playerInputHandler.movementInput;
        jumpInput = player.playerInputHandler.jumpInput;
        meditateInput = player.playerInputHandler.meditateInput;
        attackInput = player.playerInputHandler.attackInput;
        defenseInput = player.playerInputHandler.defenseInput;
        earthquakeInput = player.playerInputHandler.earthquakeInput;
        fireballInput = player.playerInputHandler.fireBallInput;

        if (jumpInput && player.playerJumpState.canJump() && !attackInput)
        {
            player.playerInputHandler.UseJumpInput();
            stateMachine.ChangeState(player.playerJumpState);
        }
        else if (meditateInput)
        {
            player.playerInputHandler.UseMeditateInput();
            stateMachine.ChangeState(player.playerMeditateState);
        }
        else if (attackInput)
        {
            player.CheckIfShouldFlip((int)input.x);
            player.playerInputHandler.UseAttackInput();
            stateMachine.ChangeState(player.playerAttackFirstState);
        }
        else if (defenseInput && statusDefense == 1)
        {
            stateMachine.ChangeState(player.playerDefState);
        }
        else if (_isHurt)
        {
            player.SetVelocityX(0);
            stateMachine.ChangeState(player.playerTakeDamageState);
        }
        else if (earthquakeInput & statusEarthquake == 1)
        {
            player.SetVelocityX(0);
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
