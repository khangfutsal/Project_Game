using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Range : Skeleton, IDmgable
{
    #region State Skeleton
    public EnemyStateMachine enemyStateMachine = new EnemyStateMachine();

    public SkeletonRange_IdleState skeletonRange_Idle;
    public SkeletonRange_MoveState skeletonRange_Move;
    public SkeletonRange_DefenseState skeletonRange_Defense;
    public SkeletonRange_AttackState skeletonRange_Attack;
    public SkeletonRange_DeathState skeletonRange_Death;
    public SkeletonRange_TakeDamageState skeletonRange_TakeDamage;
    public SkeletonRange_ReviveState skeletonRange_Revive;

    public SkeletonRange_Data skeletonRange_Data;

    private SkeletonRange_Bullet bullet;

    #endregion

    #region Variable Component
    [Header("Colliders")]
    [Space()]

    private BoxCollider2D boxCollider2D;
    private Object_Pool objPool;

    #region Transform Object
    [Space(5)]
    [SerializeField] private Transform groundCheckTf;
    [SerializeField] private Transform hitboxTf;
    #endregion

    #endregion

    #region Other Variables
    [Header("Damage Attack FlyingEye Melee")]
    [SerializeField] private float dmgAttack;

    private bool _isTakeDamage;
    private bool _isFlip;
    private bool _isDeath;
    private bool _isFired;
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }

    #endregion

    #region Initialize List

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        rgBody2D = transform.parent.GetComponentInChildren<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;
        objPool = transform.GetComponent<Object_Pool>();
        enemyStateMachine = new EnemyStateMachine();

        skeletonRange_Revive = new SkeletonRange_ReviveState(this, enemyStateMachine, skeletonRange_Data, "revive");
        skeletonRange_Idle = new SkeletonRange_IdleState(this, enemyStateMachine, skeletonRange_Data, "idle");
        skeletonRange_Move = new SkeletonRange_MoveState(this, enemyStateMachine, skeletonRange_Data, "move");
        skeletonRange_Death = new SkeletonRange_DeathState(this, enemyStateMachine, skeletonRange_Data, "death");
        skeletonRange_Attack = new SkeletonRange_AttackState(this, enemyStateMachine, skeletonRange_Data, "attack");
        skeletonRange_Defense = new SkeletonRange_DefenseState(this, enemyStateMachine, skeletonRange_Data, "defense");
        skeletonRange_TakeDamage = new SkeletonRange_TakeDamageState(this, enemyStateMachine, skeletonRange_Data, "takedamage");

    }

    private void Start()
    {
        enemyStateMachine.Initialize(skeletonRange_Revive);

        facingDirection = 1;
        _target = 1;
        health = maxHealth;
        _isFlip = true;

        IgnoreLayerCollider();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    //anim.speed = test;
        //    FireBullet();
        //}
        float timeElapsed = anim.GetCurrentAnimatorStateInfo(0).length;
        enemyStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTf.position, skeletonRange_Data.groundCheckRadius, skeletonRange_Data.whatIsGround);
    }
    public bool CanAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, skeletonRange_Data.attackRadius, skeletonRange_Data.whatIsPlayer);
        if (collider != null)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Set Functions
    public void SetBool_IsDeath(bool _bool) => _isDeath = _bool;
    public void SetBool_IsTakeDamage(bool _bool) => _isTakeDamage = _bool;

    public void SetBool_IsFired(bool _bool) => _isFired = _bool;


    public void TakeDamage(float dmg, Transform tf = null)
    {
        health -= dmg;
        _isTakeDamage = true;

        Player player = tf.GetComponentInParent<Player>();
        bool attackSecondAlready = player.GetBool_IsHitAttackSecond();
        bool attackFinalAlready = player.GetBool_IsHitAttackFinal();
        if (attackSecondAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_9")
        {
            rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            KnockBack(10, 0);
            if (health <= 0) { Die(); health = 0; }
            return;
        }
        if (attackFinalAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_18")
        {
            rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            KnockBack(0, 5);
            StartCoroutine(player.AttackTimeScale());
            if (health <= 0) { Die(); health = 0; }
            return;
        }
        KnockBack(.5f, 0);
        if (health <= 0) { Die(); health = 0; }

    }
    #endregion

    #region Get Functions
    public float GetFloat_DmgAttack() => dmgAttack;
    public bool GetBool_IsDeath() => _isDeath;
    public bool GetBool_IsTakeDamage() => _isTakeDamage;
    public bool GetBool_IsFired() => _isFired;

    #endregion

    #region Other Functions

    public override void Chase()
    {
        CheckFlip(transform.parent.position, playerTf.position);
        // ------- Case1
        //float distance = Vector3.Distance(transform.position, playerTf.position);
        //float finalSpeed = (distance / flyingEyeMelee_Data.moveSpeed);                    
        //transform.position = Vector3.Lerp(transform.position, playerTf.position + new Vector3(1,1,1), Time.deltaTime / finalSpeed);

        // ------ Case2
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, playerTf.position, Time.deltaTime * skeletonRange_Data.moveSpeed * 5f);

        if (!_isFlip)
        {
            if (Mathf.Sign(transform.position.x - playerTf.position.x) > 0)
            {
                Flip();
                _isFlip = true;
            }
            else
            {
                Flip();
                _isFlip = true;
            }
        }
        void CheckFlip(Vector3 StartTf, Vector3 EndTf)
        {
            if (facingDirection != Mathf.Sign(StartTf.x - EndTf.x))
            {
                return;
            }
            else
            {
                _isFlip = false;
            }
        }
    }

    public IEnumerator DelayToIdleState()
    {
        yield return new WaitForSeconds(1);
        enemyStateMachine.ChangeState(skeletonRange_Idle);
    }

    public void IgnoreLayerCollider()
    {
        Physics2D.IgnoreLayerCollision(6, 9, true);
    }
    #endregion

    #region Death State
    public void Die()
    {
        rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        transform.tag = "Untagged";
        _isDeath = true;
    }

    public IEnumerator DestroyObject()
    {
        if (!_isDeath) yield break;
        _isDeath = false;

        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);
    }

    #endregion

    #region Attack State

    public void StopFireBullet()
    {
        if (bullet == null) return;
        else
        {
            bullet.gameObject.SetActive(false);
        }

    }
    public void FireBullet()
    {
        bullet = objPool.GetBulletFromPool().GetComponent<SkeletonRange_Bullet>();

        bullet.gameObject.SetActive(true);
        bullet.FireBullet(hitboxTf);
    }


    #endregion

    #region Animation Trigger
    private void AnimationTrigger() => enemyStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => enemyStateMachine.currentState.AnimationTriggerFinished();

    #endregion
}
