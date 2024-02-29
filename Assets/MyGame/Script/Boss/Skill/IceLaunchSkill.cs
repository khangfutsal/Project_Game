using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaunchSkill : BaseSkill
{
    [SerializeField] private GameObject objIce;

    [SerializeField] private GameObject PointFire;



    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSkill()
    {
        objIce.GetComponent<LaunchSkill>().timeDestroy = duration;
        objIce.GetComponent<LaunchSkill>().damage = damage;
        objIce.GetComponent<LaunchSkill>().ActiveEvent(OnHitEffect);

        var carpets = objIce.GetComponent<LaunchSkill>().carpets;
        foreach (var carpet in carpets)
        {
            carpet.GetComponent<CarpetSkill>().ActiveEvent(OnHitEffect);
        }

        objIce.SetActive(true);

        objIce.transform.position = PointFire.transform.position;
        objIce.transform.rotation = PointFire.transform.rotation;
    }


}
