using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFirst : PlayerAttackState
{
    private int dmg;
    public PlayerAttackFirst(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        dmg = UnityEngine.Random.Range(1, 3);
        GameController.GetInstance().player.SetInt_AttackDmg(dmg);
        player.StopInvulnerability();
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
            stateMachine.ChangeState(player.playerAttackSecondState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
