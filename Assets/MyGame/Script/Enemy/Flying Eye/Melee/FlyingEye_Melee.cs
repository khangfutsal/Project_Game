using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_Melee : FlyingEye, IDmgable
{
    public EnemyStateMachine enemyStateMachine = new EnemyStateMachine();

    public FlyEyeMelee_AttackState flyEyeMelee_AttackState;
    public FlyEyeMelee_MoveState flyEyeMelee_MoveState;
    public FlyEyeMelee_TakeDamageState flyEyeMelee_TakeDamageState;
    public FlyEyeMelee_DeathState flyEyeMelee_DeathState;

    public FlyingEyeMelee_Data flyingEyeMelee_Data;



    #region Variable Component

    private BoxCollider2D boxCollider2D;
    [SerializeField] public GameObject colliderEnvironment;
    #endregion

    #region Transform Obj
    [SerializeField] private Transform groundCheckTf;

    #endregion


    #region Other Variables
    [Header("Damage Attack FlyingEye Melee")]
    [SerializeField] private float dmgAttack;

    private bool _isTakeDamage;
    private bool _isFlip;
    private bool _isReturn;
    private bool _isDeath;
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }
    #endregion

    #region Unity Method

    protected override void Awake()
    {
        rgBody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;
        enemyStateMachine = new EnemyStateMachine();

        flyEyeMelee_AttackState = new FlyEyeMelee_AttackState(this, enemyStateMachine, flyingEyeMelee_Data, "attack");
        flyEyeMelee_MoveState = new FlyEyeMelee_MoveState(this, enemyStateMachine, flyingEyeMelee_Data, "move");
        flyEyeMelee_TakeDamageState = new FlyEyeMelee_TakeDamageState(this, enemyStateMachine, flyingEyeMelee_Data, "takehit");
        flyEyeMelee_DeathState = new FlyEyeMelee_DeathState(this, enemyStateMachine, flyingEyeMelee_Data, "death");
    }

    private void Start()
    {
        enemyStateMachine.Initialize(flyEyeMelee_MoveState);

        _current = .5f; 
        facingDirection = 1;
        _target = 1;
        health = maxHealth;
        _isFlip = true;

        IgnoreLayerCollider();
    }

    private void Update()
    {
        enemyStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, flyingEyeMelee_Data.radAttack);
    }

    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTf.position, flyingEyeMelee_Data.groundCheckRadius, flyingEyeMelee_Data.whatIsGround);
    }
    public bool CanAttack() => (Vector3.Distance(transform.position, playerTf.position) < .9f);

    #endregion

    #region Set Functions
    public void SetBool_IsDeath(bool _bool) => _isDeath = _bool;
    public void SetBool_IsTakeDamage(bool _bool) => _isTakeDamage = _bool;

    public void SetDefault_Moving()
    {
        if (isBound) return;

        if (_isReturn && !isBound)
        {
            ReturnPosition();
        }
        else
        {
            if (Vector3.Distance(transform.position, endPos.position) < 0.3f)
            {
                Flip();
            }
            else if (Vector3.Distance(transform.position, startPos.position) < 0.3f)
            {
                Flip();
            }
            _target = _target == 1 ? 1 : 0;
            _current = Mathf.MoveTowards(_current, _target, flyingEyeMelee_Data.moveSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp((Vector2)startPos.position, (Vector2)endPos.position, _current);
        }

        #region  
        void ReturnPosition()
        {
            CheckFlip(transform.position, startPos.position);
            if (!_isFlip)
            {
                if (Mathf.Sign(startPos.position.x - transform.position.x) > 0)
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

            transform.position = Vector3.MoveTowards(transform.position, startPos.position, Time.deltaTime * flyingEyeMelee_Data.moveSpeed * 5f);
            if (Vector3.Distance(transform.position, startPos.position) < 0.001f)
            {
                _target = 1;
                _current = 0;
                _isReturn = false;
            }

        }
        #endregion

    }

    public void IgnoreLayerCollider()
    {
        Physics2D.IgnoreLayerCollision(6, 9, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);

    }

    public void TakeDamage(float dmg, Transform tf = null)
    {
        _isTakeDamage = true;

        health -= dmg;

        if (health <= 0) { Die(); health = 0; }


        if (tf.GetComponentInParent<Player>() == null) return;

        Player player = tf.GetComponentInParent<Player>();
        bool attackFinalAlready = player.GetBool_IsHitAttackFinal();
        bool skillEarthQuakeAlready = player.GetBool_IsSkillEarthQuake();
        if (attackFinalAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_9")
        {
            KnockBack(10, 0);
            rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            return;
        }
        if (skillEarthQuakeAlready && player.GetComponent<SpriteRenderer>().sprite.name == "3_atk_18")
        {
            rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            KnockBack(0, 5);
            StartCoroutine(player.AttackTimeScale());
            return;
        }
        rgBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        KnockBack(.5f, 0);
    }
    #endregion

    #region Get Functions
    public float GetFloat_DmgAttack() => dmgAttack;
    public bool GetBool_IsDeath() => _isDeath;
    public bool GetBool_IsTakeDamage() => _isTakeDamage;

    #endregion

    #region Other Functions

    public override void Chase()
    {
        CheckFlip(transform.position, playerTf.position);
        _isReturn = true;
        // ------- Case1
        //float distance = Vector3.Distance(transform.position, playerTf.position);
        //float finalSpeed = (distance / flyingEyeMelee_Data.moveSpeed);                    
        //transform.position = Vector3.Lerp(transform.position, playerTf.position + new Vector3(1,1,1), Time.deltaTime / finalSpeed);

        // ------ Case2
        transform.position = Vector3.MoveTowards(transform.position, playerTf.position, Time.deltaTime * flyingEyeMelee_Data.moveSpeed * 5f);

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

        Collection_Controller.GetInstance().SpawnGem(transform);

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

    #region Animation Trigger
    private void AnimationTrigger() => enemyStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => enemyStateMachine.currentState.AnimationTriggerFinished();




    #endregion


}
