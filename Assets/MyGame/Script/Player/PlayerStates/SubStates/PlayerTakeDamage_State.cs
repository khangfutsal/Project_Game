using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage_State : PlayerState
{
    private bool _isKnock;
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
    }

    public override void Enter()
    {
        base.Enter();
        player.couroutine_Invulnerability = player.StartCoroutine(player.Invulnerability());
        player.KnockBack();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_isKnock)
        {
            if (Time.time >= startTime + playerData.knockDuration)
            {
                player.SetBool_IsKnock(false);
                player.SetVelocityX(0);
            }
        }
        if (isAnimationFinished)
        {
            Debug.Log("idle");
            stateMachine.ChangeState(player.playerIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
