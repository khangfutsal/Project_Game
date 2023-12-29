using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEye : Enemy
{
    public FlyingEyeData flyingEye_Data;

    [Header("Current Position")]
    [SerializeField] private Vector3 curPos;


    [Header("Initialize Direction Moving For FlyingEye")]
    [SerializeField] protected Transform startPos;
    [SerializeField] protected Transform endPos;

    public int facingDirection;
    protected float _target;
    protected float _current;

    public bool isBound;
    public bool _isKnock;
    public bool _isKnockAlready;

    public Vector2 workspace;


    #region Variable Component
    [SerializeField] public Rigidbody2D rgBody2D;
    #endregion


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
    public virtual void SetDefault_Moving()
    {
        if (_current == 1)
        {
            Flip();
        }
        _target = _target == 1 ? 1 : 0;
        _current = Mathf.MoveTowards(_current, _target, flyingEye_Data.moveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp((Vector2)startPos.position, (Vector2)endPos.position, _current);
    }

    public void KnockBack(float KnockOutX, float KnockOutY)
    {
        rgBody2D.velocity = new Vector2(KnockOutX * -facingDirection, KnockOutY);
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
