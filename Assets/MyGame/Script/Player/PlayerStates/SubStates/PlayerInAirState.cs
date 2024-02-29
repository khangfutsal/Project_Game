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
    private bool _isDurationEffectIceSkill;

    private GameObject vfxGrounded;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isHurt = player.GetBool_Hurt();
        _isDurationEffectIceSkill = player.playerStats._durationIce;

    }

    public override void Enter()
    {
        base.Enter();
        vfxGrounded = VFX_Controller.GetInstance().GetVFX_Manager().GetGroundedVFX();
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
            VFX_Controller.GetInstance().SpawnVFX(vfxGrounded, player.groundCheck, "GroundedVFX");
            stateMachine.ChangeState(player.playerIdleState);
        }
        else if (jumpInput && player.playerJumpState.canJump())
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
            if (!_isDurationEffectIceSkill)
            {
                player.anim.speed = 1;
                player.SetVelocityX(playerData.movementVelocity * xInput);
            }
            else
            {
                player.anim.speed = .2f;
                player.SetVelocityX(player.playerStats.speed * xInput);
            }
            player.CheckIfShouldFlip(xInput);

            player.anim.SetFloat("yVel", player.currentVelocity.y);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
