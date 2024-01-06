using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage_State : PlayerState
{
    private bool _isKnock;
    private bool _isDeath;
    public PlayerTakeDamage_State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        _isKnock = player.GetBool_IsKnock();
        _isDeath = player.GetBool_IsDeath();
    }

    public override void Enter()
    {
        base.Enter();
        player.couroutine_Invulnerability = player.StartCoroutine(player.Invulnerability());
        player.KnockBack(playerData.knockOutX,playerData.knockOutY);
        player.SetBool_IsHurt(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_isDeath)
        {
            stateMachine.ChangeState(player.playerDeathState);
        }
        else if (_isKnock)
        {
            if (Time.time >= startTime + playerData.knockDuration)
            {
                player.SetBool_IsKnock(false);
                player.SetVelocityX(0);
            }
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
