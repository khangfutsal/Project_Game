using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    private bool onSlope;
    private Vector2 slopePerp;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        onSlope = player.onSlope;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(input.x != 0f)
        {
            stateMachine.ChangeState(player.playerMoveState);
        }
        else if (onSlope && !jumpInput)
        {
            slopePerp = player.slopeNormalPerp;
            player.SetVelocityX(playerData.movementVelocity * slopePerp.x * -input.x);
            player.SetVelocityY(playerData.movementVelocity * slopePerp.y * -input.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
