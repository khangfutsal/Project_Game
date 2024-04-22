using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Manager : MonoBehaviour
{

    [SerializeField, Tooltip("Appear Blood When Enemies Died")] private List<GameObject> BloodsVFX;

    [Space()]
    [SerializeField] private GameObject hitVFX;

    [SerializeField] private GameObject groundedVFX;
    [SerializeField] private GameObject fireWorkVFX;

    #region Unity Method

    #endregion

    #region Get Function
    public GameObject GetGroundedVFX() => groundedVFX;
    public List<GameObject> GetBloodsVFX() => BloodsVFX;
    public GameObject GetHitVFX() => hitVFX;
    public GameObject GetFireWorkVFX() => fireWorkVFX;

    #endregion
}
