using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_AbilityState : EnemyState
{
    protected Demon demon;
    public Demon_AbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        demon = (Demon)enemy;
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


    }
}
