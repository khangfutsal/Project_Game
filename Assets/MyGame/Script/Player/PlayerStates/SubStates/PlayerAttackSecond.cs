using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSecond : PlayerAttackState
{
    public PlayerAttackSecond(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if (attackInput)
        {
            player.playerInputHandler.UseAttackInput();
            stateMachine.ChangeState(player.playerAttackThirdState);
            
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
