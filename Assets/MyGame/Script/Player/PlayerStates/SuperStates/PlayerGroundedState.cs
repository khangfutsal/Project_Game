using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;

    private bool jumpInput;
    private bool meditateInput;
    private bool attackInput;
    private bool defenseInput;

    protected bool _isHurt;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        input = player.playerInputHandler.movementInput;
        jumpInput = player.playerInputHandler.jumpInput;
        meditateInput = player.playerInputHandler.meditateInput;
        attackInput = player.playerInputHandler.attackInput;
        defenseInput = player.playerInputHandler.defenseInput;

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
        else if (defenseInput)
        {
            stateMachine.ChangeState(player.playerDefState);
        }
        else if (_isHurt)
        {
            player.SetVelocityX(0);
            stateMachine.ChangeState(player.playerTakeDamageState);
        }
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
