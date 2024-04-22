using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public PlayerDeathState playerDeathState;

    public PlayerDefState playerDefState;

    public PlayerAttackFirst playerAttackFirstState;
    public PlayerAttackSecond playerAttackSecondState;
    public PlayerAttackThird playerAttackThirdState;
    public PlayerAttackInAirState playerAttackInAirState;

    public PlayerSkillEarthQuake playerSkillEarthQuake;
    public PlayerSkillFireBall playerSkillFireBall;
    #endregion

    public Coroutine couroutine_Invulnerability;
    #region Check Transforms
    [SerializeField]
    public Transform groundCheck;
    #endregion

    #region Other Variable

    [SerializeField]
    public PlayerData playerData;
    public PlayerInputHandler playerInputHandler;
    public PlayerStats playerStats;

    public Transform hitboxTf;

    private Object_Pool objPool;

    public Vector2 workspace;
    public Vector2 currentVelocity { get; private set; }


    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    public int facingDirection { get; private set; }

    private bool _isHurt;
    private bool _isKnock;
    private bool _isInvulnerability;
    private bool _isDeath;
    private bool _isHitFinalSecond;
    private bool _isSkillEarthQuake;

    private static Player _ins;

    [SerializeField] private CameraShake cameraShake;
    [SerializeField] public Transform vfx_defense;
    [SerializeField] public Transform pointSpawnVFX;
    #endregion

    [SerializeField] private GameObject GroundedCracks;

    #region Variable Component

    public Animator anim;
    [SerializeField] public SpriteRenderer spriteRerender;
    [SerializeField] public Rigidbody2D RgBody2D;

    [SerializeField] private Material myMaterial;
    #endregion

    #region Slope Variable 
    [SerializeField] private float slopeDistance;
    [SerializeField] private LayerMask WhatIsSlope;
    [SerializeField] public Vector2 slopeNormalPerp;
    [SerializeField] private float slopeDownAngle;
    [SerializeField] private float slopeDownAngleOld;
    [SerializeField] public bool onSlope;

    #endregion

    public float curTimeScale;

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
        playerSkillEarthQuake = new PlayerSkillEarthQuake(this, playerStateMachine, playerData, "earthquake");
        playerSkillFireBall = new PlayerSkillFireBall(this, playerStateMachine, playerData, "fireball");

        objPool = GetComponent<Object_Pool>();

        _ins = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        RgBody2D = GetComponent<Rigidbody2D>();
        spriteRerender = GetComponent<SpriteRenderer>();
        myMaterial = spriteRerender.material;

        playerStateMachine.Initialize(playerIdleState);
        facingDirection = 1;
        _isDeath = false;
    }


    private void Update()
    {
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
    public bool GetBool_IsHitAttackFinal() => _isHitFinalSecond;
    public bool GetBool_IsSkillEarthQuake() => _isSkillEarthQuake;
    public bool GetBool_IsDeath() => _isDeath;
    public bool GetBool_IsKnock() => _isKnock;
    public bool GetBool_IsInvulnerability() => _isInvulnerability;
    public bool GetBool_Hurt() => _isHurt;
    public float GetTimeScale()
    {
        return curTimeScale == 0 ? 1 : curTimeScale; 
    }

    #endregion

    #region Set Functions
    public bool SetBool_IsHitAttackFinal(bool _bool) => _isHitFinalSecond = _bool;
    public bool SetBool_IsSkillEarthQuake(bool _bool) => _isSkillEarthQuake = _bool;
    public bool SetBool_IsDeath(bool _bool) => _isDeath = _bool;
    public bool SetBool__IsVulnerability(bool _bool) => _isInvulnerability = _bool;
    public bool SetBool_IsKnock(bool _bool) => _isKnock = _bool;
    public bool SetBool_IsHurt(bool _bool) => _isHurt = _bool;

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
        Color currentColor = myMaterial.GetColor("_ColorAlpha");

        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            currentColor.a = .5f;

            myMaterial.SetColor("_ColorAlpha", currentColor);
            //spriteRerender.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            currentColor.a = 1f;

            myMaterial.SetColor("_ColorAlpha", currentColor);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 11, false);


    }

    public void StopInvulnerability()
    {
        if (couroutine_Invulnerability == null) return;
        StopCoroutine(couroutine_Invulnerability);
        Color currentColor = myMaterial.GetColor("_ColorAlpha");
        currentColor.a = 1f;

        myMaterial.SetColor("_ColorAlpha", currentColor);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 11, false);
    }

    
    #endregion

    #region Hurt State Functions
    public void KnockBack(int KnockOutX, int KnockOutY)
    {
        RgBody2D.velocity = new Vector2(KnockOutX * -facingDirection, KnockOutY);
        _isKnock = true;
    }



    public IEnumerator DeleteVfX(Transform vfxDefense)
    {
        yield return new WaitForSeconds(1f);
        Destroy(vfxDefense.gameObject);
    }



    #endregion

    #region Attack State Functions
    public IEnumerator AttackTimeScale()
    {
        curTimeScale = 0.1f;

        while (curTimeScale <= 1)
        {

            Time.timeScale = curTimeScale;
            curTimeScale += Time.deltaTime * 4f;

            yield return null;
        }

        Time.timeScale = 1;


    }

    public void SpawnBullet()
    {
        FireBallBullet fireballBullet = objPool.GetTransformFromPool().GetComponent<FireBallBullet>();
        fireballBullet.gameObject.SetActive(true);
        fireballBullet.SetDirection(hitboxTf);
    }


    #endregion

    #region Function Animation Trigger
    public void AttackFinalAlready()
    {
        _isHitFinalSecond = true;
    }

    public void SkillEarthQuakeAlready()
    {
        _isSkillEarthQuake = true;
    }
    #endregion

    public void CheckInSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, slopeDistance, WhatIsSlope);
        if(hit != null)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeDownAngle != slopeDownAngleOld)
            {
                onSlope = true;
            }
            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

}
