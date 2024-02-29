using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_IdleState : Demon_AbilityState
{
    private bool _canAttack;
    public Demon_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _canAttack = demon._canAttack;

        demon.SetUpPhase();

        if (_canAttack)
        {
            stateMachine.ChangeState(demon.demon_AttackState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        demon.StartCoroutine(demon.TimeToAttack());
    }

    public override void Exit()
    {
        base.Exit();
    }


}
