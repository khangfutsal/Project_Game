using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private bool _isKnock;
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isKnock = _isKnock = player.GetBool_IsKnock();

    }

    public override void Enter()
    {
        base.Enter();
        player.KnockBack(playerData.knockOutX,playerData.knockOutY);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log(_isKnock);
        if (_isKnock)
        {
            if (Time.time >= startTime + playerData.knockDuration)
            {
                Debug.Log("Knock");
                player.SetBool_IsKnock(false);
                player.SetVelocityX(0);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
