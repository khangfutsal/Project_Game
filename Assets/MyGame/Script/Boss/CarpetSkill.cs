using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarpetSkill : MonoBehaviour
{

    [SerializeField] private GameObject leftObj;
    [SerializeField] private GameObject rightObj;
    [SerializeField] public bool isActive;
    [SerializeField] private Animator anim;

    [SerializeField] public float damage;
    public UnityEvent myEvent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator ActiveCarpet()
    {
        yield return new WaitForSeconds(.5f);
        if (leftObj != null)
        {
            if (!leftObj.GetComponent<CarpetSkill>().isActive)
                leftObj.SetActive(true);
        }
        if (rightObj != null)
        {
            if (!rightObj.GetComponent<CarpetSkill>().isActive)
                rightObj.SetActive(true);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDmgable Idmg = collision.GetComponent<IDmgable>();
            if (Idmg != null)
            {
                myEvent?.Invoke();
                Idmg.TakeDamage(damage, transform);
            }
        }
    }

    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(ActiveCarpet());
    }

    public void ActiveEvent(UnityEvent _myEvent)
    {
        myEvent = _myEvent;
    }
}
