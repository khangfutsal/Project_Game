using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_Melee : FlyingEye, IDmgable
{
    public EnemyStateMachine enemyStateMachine;

    public FlyEyeMelee_AttackState flyEyeMelee_AttackState;
    public FlyEyeMelee_MoveState flyEyeMelee_MoveState;
    public FlyEyeMelee_TakeDamageState flyEyeMelee_TakeDamageState;

    public FlyingEyeMelee_Data flyingEyeMelee_Data;

    private bool isFlip;
    private bool isReturn;
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }

    private static FlyingEye_Melee _ins;
    

    #region Variable Component
    private Rigidbody2D rg2d;


    #endregion

    #region Unity Method

    protected override void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;
        enemyStateMachine = new EnemyStateMachine();

        flyEyeMelee_AttackState = new FlyEyeMelee_AttackState(this, enemyStateMachine, flyingEyeMelee_Data, "attack");
        flyEyeMelee_MoveState = new FlyEyeMelee_MoveState(this, enemyStateMachine, flyingEyeMelee_Data, "move");
        flyEyeMelee_TakeDamageState = new FlyEyeMelee_TakeDamageState(this, enemyStateMachine, flyingEyeMelee_Data, "takehit");
    }

    private void Start()
    {
        enemyStateMachine.Initialize(flyEyeMelee_MoveState);

        facingDirection = 1;
        _target = 1;
        health = maxHealth;
        isFlip = true;
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


    public bool CanAttack() => (Vector3.Distance(transform.position, playerTf.position) < .9f);

    #endregion

    #region Set Functions
    public override void SetDefault_Moving()
    {
        if (isBound) return;

        if (isReturn && !isBound)
        {
            ReturnPosition();
        }
        else
        {
            if (Vector3.Distance(transform.position, endPos.position) < 0.001f)
            {
                Flip();
            }
            else if (Vector3.Distance(transform.position, startPos.position) < 0.001f)
            {
                Flip();
            }
            _target = _target == 1 ? 1 : 0;
            _current = Mathf.MoveTowards(_current, _target, flyingEyeMelee_Data.moveSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp((Vector2)startPos.position, (Vector2)endPos.position, _current);
        }

        void ReturnPosition()
        {
            CheckFlip(transform.position, startPos.position);
            if (!isFlip)
            {
                if (Mathf.Sign(startPos.position.x - transform.position.x) > 0)
                {
                    Flip();
                    isFlip = true;
                }
                else
                {
                    Flip();
                    isFlip = true;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, startPos.position, Time.deltaTime * flyingEyeMelee_Data.moveSpeed * 5f);
            if (Vector3.Distance(transform.position, startPos.position) < 0.001f)
            {
                _target = 1;
                _current = 0;
                isReturn = false;
            }

        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }
    #endregion

    #region Get Functions
    public static FlyingEye_Melee GetInstance() => _ins;

    #endregion

    #region Other Functions

    public override void MoveToPlayer()
    {
        CheckFlip(transform.position, playerTf.position);
        isReturn = true;
        // ------- Case1
        //float distance = Vector3.Distance(transform.position, playerTf.position);
        //float finalSpeed = (distance / flyingEyeMelee_Data.moveSpeed);                    
        //transform.position = Vector3.Lerp(transform.position, playerTf.position + new Vector3(1,1,1), Time.deltaTime / finalSpeed);

        // ------ Case2
        transform.position = Vector3.MoveTowards(transform.position, playerTf.position, Time.deltaTime * flyingEyeMelee_Data.moveSpeed * 5f);

        if (!isFlip)
        {
            if (Mathf.Sign(transform.position.x - playerTf.position.x) > 0)
            {
                Flip();
                isFlip = true;
            }
            else
            {
                Flip();
                isFlip = true;
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
            isFlip = false;
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }


    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is Attacked");
            GameController.GetInstance().player.SetBool_Hurt(true);
        }
    }
    #region Animation Trigger
    private void AnimationTrigger() => enemyStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => enemyStateMachine.currentState.AnimationTriggerFinished();


    #endregion

    
}
