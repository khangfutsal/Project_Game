using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSupport : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private float timeDestroy;
    [SerializeField] private Material material;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        StartCoroutine(SetupEffect());
    }


    public IEnumerator SetupEffect()
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);

        anim.SetBool("Trigger", true);
        material.SetVector("_HDR_Color", Color.white * 5f);
        yield return new WaitForSeconds(timeDestroy);
        anim.SetBool("ExitTrigger", true);
        


    }

    private void OnEnable()
    {
        StartCoroutine(SetupEffect());
    }

    private void OnDisable()
    {
        anim.SetBool("Trigger", false);
        anim.SetBool("ExitTrigger", false);
    }
}
