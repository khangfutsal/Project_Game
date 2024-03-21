using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_EvolutionState : EnemyState
{
    private bool isDonePhase;
    private Phase _curPhase;
    private Demon demon;
    public Demon_EvolutionState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName) : base(enemy, stateMachine, enemyData, animName)
    {
        demon = (Demon)enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDonePhase = demon.transitionDemon.isDone;
    }

    public override void Enter()
    {
        base.Enter();

        _curPhase = demon._curPhase;

        demon.SetUpPositions();
        demon.transitionDemon.StartCoroutine(demon.transitionDemon.ModifyPhase(_curPhase));
        demon.InitializePhase(_curPhase);
    }

    public override void Exit()
    {
        base.Exit();
        TransitionDemon.GetInstance().isDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDonePhase)
        {
            demon.boxCollider2D.enabled = true;
            stateMachine.ChangeState(demon.demon_IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
