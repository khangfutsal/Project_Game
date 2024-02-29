using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Controller : MonoBehaviour
{
    private static VFX_Controller _ins;

    [Header("Holder Transform")]
    [SerializeField] private Transform bloodVFXHolder;
    [SerializeField] private Transform hitVFXHolder;
    [SerializeField] private Transform GroundVFXHolder;

    [Header("Reference Object")]
    [SerializeField] private VFX_Manager VFX_Manager;

    #region Unity Method
    private void Awake()
    {
        _ins = this;
    }
    #endregion

    #region Get Function
    public static VFX_Controller GetInstance() => _ins;
    public VFX_Manager GetVFX_Manager() => VFX_Manager;
    #endregion

    #region Spawn VFX Function
    public void SpawnBloodsVFX(Transform tf)
    {
        var bloods = VFX_Manager.GetBloodsVFX();

        Object_Pool objPool = bloodVFXHolder.parent.GetComponentInChildren<Object_Pool>();

        Transform transform = objPool.GetTransformFromPool();

        if (transform != null)
        {
            transform.gameObject.SetActive(true);
            transform.position = tf.position;

            Instantiate(bloods[0], tf.position, Quaternion.identity, bloodVFXHolder);

            return;
        }
        else
        {

            foreach (var blood in bloods)
            {
                Instantiate(blood, tf.position, Quaternion.identity, bloodVFXHolder);
            }
        }

    }

    public void SpawnVFX(GameObject vfxObj, Transform tf,string name)
    {
        GameObject obj = GameObject.Find(name);
        Transform holder = obj.transform.Find("Holder");
        Object_Pool objPool = holder.parent.GetComponentInChildren<Object_Pool>();

        Transform transform = objPool.GetTransformFromPool();

        if (transform != null)
        {
            transform.gameObject.SetActive(true);
            transform.position = tf.position;
            return;
        }
        else Instantiate(vfxObj, tf.position, Quaternion.identity, holder);

    }

    #endregion
}
