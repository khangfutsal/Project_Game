using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Animator anim;
    public EnemyData enemyData;
    public Transform playerTf;

    public int facingDirection;
    protected float _target;

    public bool isBound;
    public bool _isKnock;
    public bool _isKnockAlready;

    public Vector2 workspace;



    #region Variable Component
    [SerializeField] public Rigidbody2D rgBody2D;
    #endregion

    protected virtual void Awake()
    {
        
    }

    #region Set Function
    public void SetBound(bool boolean) => isBound = boolean;
    public bool SetBool_IsKnock(bool _bool) => _isKnock = _bool;
    public bool SetBool_IsKnockAlready(bool _bool) => _isKnockAlready = _bool;
    #endregion

    #region Get Function
    public bool GetBool_IsKnock() => _isKnock;
    public bool GetBool_IsKnockAlready() => _isKnockAlready;
    #endregion

    #region Other Function

    public void Flip()
    {
        facingDirection *= -1;
        switch (facingDirection)
        {
            case 1: _target = 1; break;
            case -1: _target = 0; break;
        }
        transform.Rotate(0.0f, 180f, 0.0f);
    }



    public void KnockBack(float KnockOutX, float KnockOutY)
    {
        Player player = playerTf.GetComponent<Player>();
        int facing = player.facingDirection;
        _isKnock = true;
        rgBody2D.velocity = new Vector2(KnockOutX * facing, KnockOutY);
    }

    public void SetVelocityX(float velocity)
    {
        //workspace.Set(velocity, currentVelocity.y);
        rgBody2D.velocity = workspace;
        //currentVelocity = workspace;

        _isKnockAlready = true;
    }


    public abstract void MoveToPlayer();

    #endregion
}
