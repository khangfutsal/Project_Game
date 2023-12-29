using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{ 
    private bool _isDeath;
    private bool defenseInput;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isDeath = player.GetBool_Hurt();
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
        defenseInput = player.playerInputHandler.defenseInput;

        if (!_isHurt && !_isDeath && !defenseInput)
        {
            player.CheckIfShouldFlip((int)input.x);
            player.SetVelocityX(playerData.movementVelocity * input.x);

            if (input.x == 0f)
            {
                stateMachine.ChangeState(player.playerIdleState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
