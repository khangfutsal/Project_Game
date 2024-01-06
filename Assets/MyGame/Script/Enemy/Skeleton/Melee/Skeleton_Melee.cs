using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Melee : Skeleton, IDmgable
{

    #region State Skeleton
    public EnemyStateMachine enemyStateMachine = new EnemyStateMachine();

    public SkeletonMelee_IdleState skeletonMelee_Idle;
    public SkeletonMelee_MoveState skeletonMelee_Move;
    public SkeletonMelee_AttackState skeletonMelee_Attack;
    public SkeletonMelee_TakeDamageState skeletonMelee_TakeDamage;
    public SkeletonMelee_DeathState skeletonMelee_Death;
    public SkeletonMelee_Defense skeletonMelee_Defense;

    public SkeletonMelee_Data skeletonMelee_Data;

    #endregion
    #region Variable Component
    [SerializeField] private Transform groundCheckTf;
    [SerializeField] private BoxCollider2D boxCollider2D;
    #endregion

    #region Other Variables
    [SerializeField] private bool _isTakeDamage;
    [Header("Damage Attack FlyingEye Melee")]
    [SerializeField] private float dmgAttack;

    private bool _isFlip;
    private bool _isReturn;
    private bool _isDeath;
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }
    #endregion


    #region Unity Method
    private void Awake()
    {
        rgBody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;

        skeletonMelee_Idle = new SkeletonMelee_IdleState(this, enemyStateMachine, skeletonMelee_Data, "idle");
        skeletonMelee_Move = new SkeletonMelee_MoveState(this, enemyStateMachine, skeletonMelee_Data, "move");
        skeletonMelee_Attack = new SkeletonMelee_AttackState(this, enemyStateMachine, skeletonMelee_Data, "attack");
        skeletonMelee_TakeDamage = new SkeletonMelee_TakeDamageState(this, enemyStateMachine, skeletonMelee_Data, "takedamage");
        skeletonMelee_Death = new SkeletonMelee_DeathState(this, enemyStateMachine, skeletonMelee_Data, "death");
        skeletonMelee_Defense = new SkeletonMelee_Defense(this, enemyStateMachine, skeletonMelee_Data, "defense");

    }

    private void Start()
    {
        enemyStateMachine.Initialize(skeletonMelee_Idle);

        facingDirection = 1;
        _target = 1;
        health = maxHealth;
        _isFlip = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Destroy(transform.parent.gameObject);
        }
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
        return Physics2D.OverlapCircle(groundCheckTf.position, skeletonMelee_Data.groundCheckRadius, skeletonMelee_Data.whatIsGround);
    }
    public bool CanAttack() => (Vector3.Distance(transform.position, playerTf.position) < .9f);

    #endregion

    #region Set Functions
    public void SetBool_IsDeath(bool _bool) => _isDeath = _bool;
    public void SetBool_IsTakeDamage(bool _bool) => _isTakeDamage = _bool;

    public void TakeDamage(float dmg,Transform tf)
    {
        health -= dmg;
        _isTakeDamage = true;

        Player player = tf.GetComponent<Player>();
        bool attackSecondAlready = player.GetBool_IsHitAttackSecond();
        bool attackFinalAlready = player.GetBool_IsHitAttackFinal();
        if (attackSecondAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_9")
        {
            KnockBack(10, 0);
            rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
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
        rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        KnockBack(.5f, 0);
        if (health <= 0) { Die(); health = 0; }
    }
    #endregion

    #region Get Functions
    public float GetFloat_DmgAttack() => dmgAttack;
    public bool GetBool_IsDeath() => _isDeath;
    public bool GetBool_IsTakeDamage() => _isTakeDamage;

    #endregion

    #region Other Functions

    public override void MoveToPlayer()
    {
        CheckFlip(transform.position, playerTf.position);
        _isReturn = true;

        transform.position = Vector3.MoveTowards(transform.position, playerTf.position, Time.deltaTime * skeletonMelee_Data.moveSpeed * 5f);

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
    

    #endregion

    #region Death State
    public void Die()
    {
        rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        boxCollider2D.enabled = false;
        _isDeath = true;
    }

    public IEnumerator DestroyObject()
    {
        if (!_isDeath) yield break;
        _isDeath = false;

        rgBody2D.bodyType = RigidbodyType2D.Static;

        Debug.Log("Test");

        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);
    }

    #endregion

    #region Animation Trigger
    private void AnimationTrigger() => enemyStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => enemyStateMachine.currentState.AnimationTriggerFinished();




    #endregion
}
