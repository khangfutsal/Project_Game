using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;
    protected bool isAnimationFinished;

    protected float startTime;

    private string animName;

    
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animName = animName;
    }

    public virtual void DoChecks()
    {

    }

    public virtual void Enter()
    {
        DoChecks();
        enemy.anim.SetBool(animName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        GameController.GetInstance().player.SetBool_AttackEnemy(false);
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void AnimationTrigger() { }
    public virtual void AnimationTriggerFinished() => isAnimationFinished = true;


    
}
