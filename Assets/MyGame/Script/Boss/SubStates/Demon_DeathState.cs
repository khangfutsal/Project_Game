using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_DeathState : EnemyState
{
    private Demon demon;
    public Demon_DeathState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
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
        demon.skillsList.ForEach((_) =>
        {
            if (_.name == "SummonEnemies")
            {
                _.GetComponent<SummonSkill>().UselessSkill();
            }
        });

        demon.TransitionDeath();




    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
