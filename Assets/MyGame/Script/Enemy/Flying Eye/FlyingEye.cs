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

    protected float facingDirection;
    protected float _target;
    protected float _current;

    public bool isBound;


    #region Set Function
    public void SetBound(bool boolean) => isBound = boolean;
    #endregion

    #region Other Function

    public void Flip()
    {
        facingDirection *= -1;
        switch (facingDirection)
        {
            case 1:_target = 1;break;
            case -1:_target = 0;break;
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

    public abstract void MoveToPlayer();

    #endregion

}
