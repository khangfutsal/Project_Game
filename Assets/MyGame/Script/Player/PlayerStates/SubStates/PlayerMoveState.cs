using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private bool _isDeath;
    private bool defenseInput;
    private bool _isDurationEffectIceSkill;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isDeath = player.GetBool_Hurt();
        _isDurationEffectIceSkill = player.playerStats._durationIce;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocityX(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        defenseInput = player.playerInputHandler.defenseInput;

        if (!_isHurt && !_isDeath && !defenseInput)
        {
            player.CheckIfShouldFlip((int)input.x);
            if (!_isDurationEffectIceSkill)
            {
                player.anim.speed = 1;
                player.SetVelocityX(playerData.movementVelocity * input.x);
            }
            else
            {
                player.anim.speed = .2f;
                player.SetVelocityX(player.playerStats.speed * input.x);
            }


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
