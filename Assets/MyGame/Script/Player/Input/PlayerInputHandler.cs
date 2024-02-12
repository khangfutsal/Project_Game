using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movementInput;

    public bool jumpInput;
    [SerializeField]
    private float inputHoldtime = 0.2f;
    private float jumpInputStartTime;


    public bool meditateInput;
    public bool attackInput;
    public bool defenseInput;
    public bool earthquakeInput;
    public bool fireBallInput;

    [SerializeField] private float skillFireballMana;
    [SerializeField] private float skillEarthQuakeMana;

    private PlayerStats playerStats;
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        OnMoveInput();
        OnJumpInput();
        OnMeditateInput();
        OnAttackInput();
        OnDefenseInput();
        OnSkillEarthQuakeInput();
        OnSkillFireBallInput();

        CheckJumpInputHoldTime();
    }
    #region Attack Function
    private void OnAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackInput = true;
        }
    }

    public void UseAttackInput() => attackInput = false;
    #endregion


    #region Skill EarthQuake Function
    public void OnSkillEarthQuakeInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerStats.mana >= skillEarthQuakeMana && playerStats.GetFloat_StatusEarthquake() == 1)
        {
            HudUI.GetInstance().TakeSliderMana(skillEarthQuakeMana);
            earthquakeInput = true;

            playerStats.TakeMana(skillEarthQuakeMana);
        }
    }

    public void UseSkillEarthQuakeInput() => earthquakeInput = false;
    #endregion

    #region FireBall Function
    public void OnSkillFireBallInput()
    {

        if (Input.GetKeyDown(KeyCode.Z) && playerStats.mana >= skillFireballMana && playerStats.GetFloat_StatusFireBall() == 1)
        {
            HudUI.GetInstance().TakeSliderMana(skillFireballMana);
            fireBallInput = true;
            playerStats.TakeMana(skillFireballMana);
        }


    }
    public void UseSkillFireBallInput() => fireBallInput = false;

    #endregion

    #region Defense Function
    public void OnDefenseInput()
    {
        if (Input.GetMouseButton(1))
        {
            defenseInput = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            defenseInput = false;
        }
    }
    #endregion

    #region Meditate Function
    private void OnMeditateInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            meditateInput = true;
        }
    }

    public void UseMeditateInput() => meditateInput = false;
    #endregion

    #region Move Function
    public void OnMoveInput()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        movementInput.Set(inputX, inputY);
    }
    #endregion

    #region Jump Function
    public void OnJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldtime)
        {
            jumpInput = false;
        }
    }

    public void UseJumpInput() => jumpInput = false;
    #endregion


}
