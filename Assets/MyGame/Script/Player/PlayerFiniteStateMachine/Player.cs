using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Player Animation
    public PlayerStateMachine playerStateMachine;

    public PlayerIdleState playerIdleState;

    public PlayerMoveState playerMoveState;

    public PlayerJumpState playerJumpState;

    public PlayerInAirState playerInAirState;

    public PlayerMeditateState playerMeditateState;

    public PlayerTakeDamage_State playerTakeDamageState;

    public PlayerAttackFirst playerAttackFirstState;
    public PlayerAttackSecond playerAttackSecondState;
    public PlayerAttackThird playerAttackThirdState;
    public PlayerAttackInAirState playerAttackInAirState;
    #endregion

    public Coroutine couroutine_Invulnerability;
    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    #endregion

    #region Other Variable

    [SerializeField]
    public PlayerData playerData;
    public PlayerInputHandler playerInputHandler;

    public Vector2 workspace;
    public Vector2 currentVelocity { get; private set; }


    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    public int facingDirection { get; private set; }
    public int attackDmg;

    private float time;

    private bool _isAttackEnemy;
    private bool _isHurt;
    private bool _isKnock;
    private bool _isInvulnerability;
    private bool _test;

    private static Player _ins;

    [SerializeField] private CameraShake cameraShake;
    #endregion

    #region Variable Component

    public Animator anim;
    [SerializeField] public SpriteRenderer spriteRerender;
    [SerializeField] private Rigidbody2D RgBody2D;
    #endregion

    #region Attack Variable

    #endregion

    #region Unity Method
    private void Awake()
    {
        playerStateMachine = new PlayerStateMachine();

        playerIdleState = new PlayerIdleState(this, playerStateMachine, playerData, "idle");
        playerMoveState = new PlayerMoveState(this, playerStateMachine, playerData, "move");
        playerJumpState = new PlayerJumpState(this, playerStateMachine, playerData, "inAir");
        playerInAirState = new PlayerInAirState(this, playerStateMachine, playerData, "inAir");
        playerMeditateState = new PlayerMeditateState(this, playerStateMachine, playerData, "meditate");
        playerTakeDamageState = new PlayerTakeDamage_State(this, playerStateMachine, playerData, "takedamage");
        playerAttackFirstState = new PlayerAttackFirst(this, playerStateMachine, playerData, "attack1");
        playerAttackSecondState = new PlayerAttackSecond(this, playerStateMachine, playerData, "attack2");
        playerAttackThirdState = new PlayerAttackThird(this, playerStateMachine, playerData, "attack3");
        playerAttackInAirState = new PlayerAttackInAirState(this, playerStateMachine, playerData, "attackAir");

        _ins = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        RgBody2D = GetComponent<Rigidbody2D>();
        spriteRerender = GetComponent<SpriteRenderer>();
        playerStateMachine.Initialize(playerIdleState);
        facingDirection = 1;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            

        }
        currentVelocity = RgBody2D.velocity;
        playerStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        playerStateMachine.currentState.PhysicsUpdate();
    }

    #endregion

    #region Get Functions
    public static Player GetInstance() => _ins;
    public bool GetBool_IsKnock() => _isKnock;
    public bool GetBool_IsInvulnerability() => _isInvulnerability;
    public bool GetBool_Hurt() => _isHurt;
    public bool GetBool_AttackEnemy() => _isAttackEnemy;
    public int GetInt_AttackDmg() => attackDmg;
    #endregion

    #region Set Functions
    public bool SetBool__IsVulnerability(bool _bool) => _isInvulnerability = _bool;
    public bool SetBool_IsKnock(bool _bool) => _isKnock = _bool;
    public bool SetBool_Hurt(bool _bool) => _isHurt = _bool;
    public bool SetBool_AttackEnemy(bool _bool) => _isAttackEnemy = _bool;
    public int SetInt_AttackDmg(int dmg) => attackDmg = dmg;

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        RgBody2D.velocity = workspace;
        currentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        RgBody2D.velocity = workspace;
        currentVelocity = workspace;
    }

    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => playerStateMachine.currentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => playerStateMachine.currentState.AnimationFinishTrigger();

    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);

    }

    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRerender.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRerender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            Debug.Log("aa");
        }
        Physics2D.IgnoreLayerCollision(6, 8, false);

    }

    public void StopInvulnerability()
    {
        if (couroutine_Invulnerability == null) return;
        StopCoroutine(couroutine_Invulnerability);
        spriteRerender.color = Color.white;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    #endregion

    #region Hurt State Functions
    public void KnockBack()
    {
        RgBody2D.velocity = new Vector2(playerData.knockOutX * -facingDirection, playerData.knockOutY);
        _isKnock = true;
        time = Time.time;
    }
    #endregion

}
