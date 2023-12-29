using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDmgable
{
    #region Player Animation
    public PlayerStateMachine playerStateMachine;

    public PlayerIdleState playerIdleState;

    public PlayerMoveState playerMoveState;

    public PlayerJumpState playerJumpState;

    public PlayerInAirState playerInAirState;

    public PlayerMeditateState playerMeditateState;

    public PlayerTakeDamage_State playerTakeDamageState;

    public PlayerDeathState playerDeathState;

    public PlayerDefState playerDefState;

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
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }

    private int attackDmg;

    private float time;

    private bool _isAttackEnemy;
    private bool _isHurt;
    private bool _isKnock;
    private bool _isInvulnerability;
    private bool _isDeath;
    private bool _isHitAttackSecond;
    private bool _isHitAttackFinal;

    private static Player _ins;

    [SerializeField] private CameraShake cameraShake;
    [SerializeField] public Transform vfx_defense;
    [SerializeField] public Transform pointSpawnVFX;
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
        playerDeathState = new PlayerDeathState(this, playerStateMachine, playerData, "Death");
        playerDefState = new PlayerDefState(this, playerStateMachine, playerData, "def");
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
        
        health = maxHealth;
        facingDirection = 1;
        _isDeath = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetVelocityX(0);
            //SceneManager.LoadScene("Khang");
            //Time.timeScale = 1;

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
    public bool GetBool_IsHitAttackSecond() => _isHitAttackSecond;
    public bool GetBool_IsHitAttackFinal() => _isHitAttackFinal;
    public bool GetBool_IsDeath() => _isDeath;
    public bool GetBool_IsKnock() => _isKnock;
    public bool GetBool_IsInvulnerability() => _isInvulnerability;
    public bool GetBool_Hurt() => _isHurt;
    public bool GetBool_AttackEnemy() => _isAttackEnemy;
    public int GetInt_AttackDmg() => attackDmg;
    #endregion

    #region Set Functions
    public bool SetBool_IsHitAttackSecond(bool _bool) => _isHitAttackSecond = _bool;
    public bool SetBool_IsHitAttackFinal(bool _bool) => _isHitAttackFinal = _bool;
    public bool SetBool_IsDeath(bool _bool) => _isDeath = _bool;
    public bool SetBool__IsVulnerability(bool _bool) => _isInvulnerability = _bool;
    public bool SetBool_IsKnock(bool _bool) => _isKnock = _bool;
    public bool SetBool_IsHurt(bool _bool) => _isHurt = _bool;
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
        }
        Physics2D.IgnoreLayerCollision(6, 8, false);


    }

    public void StopInvulnerability()
    {
        if (couroutine_Invulnerability == null) return;
        StopCoroutine(couroutine_Invulnerability);
        Debug.Log("Stop");  
        spriteRerender.color = Color.white;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    #endregion

    #region Hurt State Functions
    public void KnockBack(int KnockOutX, int KnockOutY)
    {
        RgBody2D.velocity = new Vector2(KnockOutX * -facingDirection, KnockOutY);
        _isKnock = true;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) { Die(); health = 0; }
    }

    public void Die()
    {
        SetBool_IsDeath(true);
        SetVelocityX(0);
        StartCoroutine(DelayEnd());
        IEnumerator DelayEnd()
        {
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
        }
    }

    public IEnumerator DeleteVfX(Transform vfxDefense)
    {
        yield return new WaitForSeconds(1f);
        Destroy(vfxDefense.gameObject);
    }



    #endregion

    #region Function Animation Trigger
    public void AttackSecondAlready()
    {
        _isHitAttackSecond = true;
    }

    public void AttackFinalAlready()
    {
        _isHitAttackFinal = true;
    }
    #endregion
}
