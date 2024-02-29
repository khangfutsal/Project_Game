using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_AttackState : Demon_AbilityState
{
    public Demon_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        MaterialPhase.GetInstance().DisappearDissolve();
    }

    public override void Exit()
    {
        base.Exit();
        demon._endSkill = false;
        demon._canAttack = false;
        demon.anim.SetBool("Mantra", false);
        demon.anim.SetBool("Launch", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (demon._endSkill)
        {
            stateMachine.ChangeState(demon.demon_IdleState);
            demon.SetUpPhase();

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
