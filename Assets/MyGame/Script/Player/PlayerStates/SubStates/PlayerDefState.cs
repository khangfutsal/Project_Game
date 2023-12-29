using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefState : PlayerState
{
    private bool defenseInput;
    private bool _isHurt;
    private bool _isKnock;
    private float _startTime;
    public PlayerDefState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);

        _startTime = startTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        defenseInput = player.playerInputHandler.defenseInput;

        _isHurt = player.GetBool_Hurt();
        _isKnock = player.GetBool_IsKnock();

        if (_isHurt)
        {
            player.KnockBack(5,0);
            player.SetBool_IsHurt(false);
            _startTime = Time.time; ;

            Transform vfx = Transform.Instantiate(player.vfx_defense,
                player.pointSpawnVFX.position,
                Quaternion.Euler(player.vfx_defense.localRotation.x, player.transform.localEulerAngles.y, player.vfx_defense.localRotation.z));
            player.StartCoroutine(player.DeleteVfX(vfx));
            
        }
        if (_isKnock)
        {
            if (Time.time >= _startTime + .1f)
            {
                player.SetBool_IsKnock(false);
                player.SetVelocityX(0);
            }
        }
        if (!defenseInput)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
