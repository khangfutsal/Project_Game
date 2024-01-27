using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Manager : MonoBehaviour
{

    [SerializeField, Tooltip("Appear Blood When Enemies Died")] private List<GameObject> BloodsVFX;

    [Space()]
    [SerializeField] private GameObject hitVFX;

    #region Unity Method

    #endregion

    #region Get Function
    public List<GameObject> GetBloodsVFX() => BloodsVFX;
    public GameObject GetHitVFX() => hitVFX;
    #endregion
}
