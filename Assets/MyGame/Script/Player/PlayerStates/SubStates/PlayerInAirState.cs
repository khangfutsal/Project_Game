using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool attackInput;
    private bool isHurt;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isHurt = player.GetBool_Hurt();

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

        xInput = (int)player.playerInputHandler.movementInput.x;
        jumpInput = player.playerInputHandler.jumpInput;
        attackInput = player.playerInputHandler.attackInput;

        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
        else if(jumpInput && player.playerJumpState.canJump())
        {
            stateMachine.ChangeState(player.playerJumpState);
        }
        else if (attackInput)
        {
            player.playerInputHandler.UseAttackInput();
            stateMachine.ChangeState(player.playerAttackInAirState);
        }
        else if (isHurt)
        {
            stateMachine.ChangeState(player.playerTakeDamageState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            player.anim.SetFloat("yVel", player.currentVelocity.y);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
