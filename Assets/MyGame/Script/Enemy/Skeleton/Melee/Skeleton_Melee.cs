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
    public SkeletonMelee_ReviveState skeletonMelee_Revive;

    [Header("Properties")]
    [Space()]
    public SkeletonMelee_Data skeletonMelee_Data;


    #region Other Variables
    [Header("Damage Attack FlyingEye Melee")]
    [SerializeField] private float dmgAttack;

    private bool _isTakeDamage;
    private bool _isFlip;
    private bool _isDefense;
    private bool _isDeath;
    private bool _useDecisionNextState;

    
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }
    #endregion


    #endregion
    #region Variable Component
    [SerializeField] private Transform groundCheckTf;
    
    private BoxCollider2D boxCollider2D;

    #endregion



    #region Unity Method
    private void Awake()
    {
        rgBody2D = transform.parent.GetComponentInChildren<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        anim = transform.GetComponentInChildren<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;

        skeletonMelee_Idle = new SkeletonMelee_IdleState(this, enemyStateMachine, skeletonMelee_Data, "idle");
        skeletonMelee_Move = new SkeletonMelee_MoveState(this, enemyStateMachine, skeletonMelee_Data, "move");
        skeletonMelee_Attack = new SkeletonMelee_AttackState(this, enemyStateMachine, skeletonMelee_Data, "attack");
        skeletonMelee_TakeDamage = new SkeletonMelee_TakeDamageState(this, enemyStateMachine, skeletonMelee_Data, "takedamage");
        skeletonMelee_Death = new SkeletonMelee_DeathState(this, enemyStateMachine, skeletonMelee_Data, "death");
        skeletonMelee_Defense = new SkeletonMelee_Defense(this, enemyStateMachine, skeletonMelee_Data, "defense");
        skeletonMelee_Revive = new SkeletonMelee_ReviveState(this, enemyStateMachine, skeletonMelee_Data, "revive");

    }

    private void Start()
    {
        enemyStateMachine.Initialize(skeletonMelee_Revive);

        facingDirection = 1;
        _target = 1;
        health = maxHealth;
        _isFlip = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            KnockBack(10, 0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            KnockBack(0, 5);
        }
        //if (enemyStateMachine.currentState == null) return;

        enemyStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        //if (enemyStateMachine.currentState == null) return;
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
    public void SetBool_DecisionNextState(bool _bool) => _useDecisionNextState = _bool;
    public void SetBool_IsDefense(bool _bool) => _isDefense = _bool;
    public void SetBool_IsDeath(bool _bool) => _isDeath = _bool;
    public void SetBool_IsTakeDamage(bool _bool) => _isTakeDamage = _bool;
    #endregion

    #region Get Functions
    public bool GetBool_DecisionNextState() => _useDecisionNextState;
    public float GetFloat_DmgAttack() => dmgAttack;
    public bool GetBool_IsDefense() => _isDefense;
    public bool GetBool_IsDeath() => _isDeath;
    public bool GetBool_IsTakeDamage() => _isTakeDamage;

    #endregion
    public void TakeDamage(float dmg, Transform tf = null)
    {
        _isTakeDamage = true;

        float totalDamage = dmg;
        Player player = tf.GetComponentInParent<Player>();

        float playerFacingDirection = player.facingDirection;
        if (_isDefense)
        {
            if (facingDirection != playerFacingDirection)
            {
                totalDamage = dmg * .8f;
                health -= totalDamage;
            }
            else
            {
                health -= totalDamage;
                enemyStateMachine.ChangeState(skeletonMelee_TakeDamage);
            }
        }
        else
        {
            health -= totalDamage;
        }
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
        rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        KnockBack(.5f, 0);
        if (health <= 0) { Die(); health = 0; }
    }




    #region Other Functions

    public void DecisionNextState()
    {
        _useDecisionNextState = true;

        float randomValue = Random.value;
        if (randomValue < 0.3f)
        {
            SwitchDef_State();
        }
        else
        {
            SwitchAttack_State();
        }
    }

    public void SwitchDef_State()
    {
        _useDecisionNextState = false;
        enemyStateMachine.ChangeState(skeletonMelee_Defense);
    }

    public void SwitchAttack_State()
    {
        _useDecisionNextState = false;
        enemyStateMachine.ChangeState(skeletonMelee_Attack);
    }

    public override void Chase()
    {
        CheckFlip(transform.parent.position, playerTf.position);

        transform.parent.position = Vector3.MoveTowards(transform.parent.position, playerTf.position, Time.deltaTime * skeletonMelee_Data.moveSpeed * 5f);

        if (!_isFlip)
        {
            if (Mathf.Sign(transform.parent.position.x - playerTf.position.x) > 0)
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
        enemyStateMachine.ChangeState(skeletonMelee_Idle);
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

    #region Defense State
    public float DefenseHoldTime(float from, float to)
    {
        return UnityEngine.Random.Range(from, to);
    }
    #endregion

    #region Animation Trigger
    private void AnimationTrigger() => enemyStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => enemyStateMachine.currentState.AnimationTriggerFinished();




    #endregion
}
