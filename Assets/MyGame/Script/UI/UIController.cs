using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public UIManager uiManager;
    private static UIController _ins;


    #region Get Function
    public static UIController GetInstance() => _ins;
    #endregion
    private void Awake()
    {
        _ins = this;
    }


}
