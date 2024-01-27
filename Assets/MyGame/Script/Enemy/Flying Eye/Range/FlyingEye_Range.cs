using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_Range : FlyingEye, IDmgable
{
    #region State FlyingEye
    public EnemyStateMachine enemyStateMachine = new EnemyStateMachine();

    public FlyEyeRange_AttackState flyEyeRange_AttackState;
    public FlyEyeRange_MoveState flyEyeRange_MoveState;
    public FlyEyeRange_TakeDamageState flyEyeRange_TakeDamageState;
    public FlyEyeRange_DeathState flyEyeRange_DeathState;

    public FlyingEyeRange_Data flyingEyeRange_Data;

    private FlyEyeRange_Bullet bullet;

    private Object_Pool objPool;
    #endregion

    #region Variable Component

    private BoxCollider2D boxCollider2D;

    #endregion

    #region Transform Obj

    [SerializeField] private Transform groundCheckTf;
    [SerializeField] private Transform hitboxTf;

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

    public float test;
    #endregion

    #region Unity Method

    protected override void Awake()
    {

        rgBody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;
        objPool = transform.GetComponent<Object_Pool>();
        enemyStateMachine = new EnemyStateMachine();

        flyEyeRange_AttackState = new FlyEyeRange_AttackState(this, enemyStateMachine, flyingEyeRange_Data, "attack");
        flyEyeRange_MoveState = new FlyEyeRange_MoveState(this, enemyStateMachine, flyingEyeRange_Data, "move");
        flyEyeRange_TakeDamageState = new FlyEyeRange_TakeDamageState(this, enemyStateMachine, flyingEyeRange_Data, "takehit");
        flyEyeRange_DeathState = new FlyEyeRange_DeathState(this, enemyStateMachine, flyingEyeRange_Data, "death");
    }

    private void Start()
    {
        enemyStateMachine.Initialize(flyEyeRange_MoveState);

        facingDirection = 1;
        _target = 1;
        health = maxHealth;
        _isFlip = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.speed = test;
        }
        enemyStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, flyingEyeMelee_Data.radAttack);
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Player is Attacked");
    //        float dmg = GetFloat_DmgAttack();

    //        Player player = collision.gameObject.GetComponent<Player>();
    //        player.SetBool_IsHurt(true);
    //        player.TakeDamage(dmg);
    //    }
    //}
    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTf.position, flyingEyeRange_Data.groundCheckRadius, flyingEyeRange_Data.whatIsGround);
    }
    public bool CanAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, flyingEyeRange_Data.attackRadius, flyingEyeRange_Data.whatIsPlayer);
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
        CheckFlip(transform.position, playerTf.position);
        // ------- Case1
        //float distance = Vector3.Distance(transform.position, playerTf.position);
        //float finalSpeed = (distance / flyingEyeMelee_Data.moveSpeed);                    
        //transform.position = Vector3.Lerp(transform.position, playerTf.position + new Vector3(1,1,1), Time.deltaTime / finalSpeed);

        // ------ Case2
        Vector3 targetPlayer = new Vector3(playerTf.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPlayer, Time.deltaTime * flyingEyeRange_Data.moveSpeed * 5f);

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
    }
    public void CheckFlip(Vector3 StartTf, Vector3 EndTf)
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

    #endregion

    #region Death State
    public void Die()
    {
        VFX_Controller.GetInstance().SpawnBloodsVFX(transform);

        rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        boxCollider2D.enabled = false;
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
            bullet.StopFireBullet();
            bullet.gameObject.SetActive(false);
        }

    }
    public void FireBullet()
    {
        bullet = objPool.GetTransformFromPool().GetComponent<FlyEyeRange_Bullet>();
        bullet.transform.position = hitboxTf.position;
        bullet.gameObject.SetActive(true);
        bullet.FireBullet();
    }

    #endregion

    #region Animation Trigger
    private void AnimationTrigger() => enemyStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => enemyStateMachine.currentState.AnimationTriggerFinished();


    #endregion
}
