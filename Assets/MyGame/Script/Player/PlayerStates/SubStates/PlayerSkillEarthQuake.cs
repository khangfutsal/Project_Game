using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEarthQuake : PlayerState
{
    public PlayerSkillEarthQuake(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerInputHandler.UseSkillEarthQuakeInput();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);

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
