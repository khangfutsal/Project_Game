using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{

    public bool attackInput;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.SetVelocityX(0);
        attackInput = player.playerInputHandler.attackInput;

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }   
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    
}
