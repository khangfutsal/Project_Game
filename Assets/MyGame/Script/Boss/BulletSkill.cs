using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : MonoBehaviour
{

    [SerializeField] public float damage;
    [SerializeField] public float timeDestroy;
    [SerializeField] public Rigidbody2D rgbody2D;
    [SerializeField] public bool _interactWall;
    [SerializeField] public bool _interactGrounded;
    [SerializeField] private bool isExplode;
    [SerializeField] private float time;

    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    //private void Update()
    //{
    //    isExplode = anim.GetCurrentAnimatorStateInfo(0).IsName("Explode");
    //    if (isExplode)
    //    {
    //        time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    //        if (time >= 1f)
    //        {
    //            InActive();
    //        }
    //    }
    //}




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || (collision.CompareTag("Wall") && _interactWall) || (collision.CompareTag("Grounded") && _interactGrounded))
        {
            anim.SetBool("Explode", true);
            Invoke("InActive", timeDestroy);
            rgbody2D.bodyType = RigidbodyType2D.Static;
            boxCollider2D.enabled = false;

            IDmgable Idmg = collision.GetComponent<IDmgable>();
            if (Idmg != null)
            {
                Idmg.TakeDamage(damage, transform);
            }
        }
    }


    private void OnEnable()
    {
        rgbody2D.bodyType = RigidbodyType2D.Kinematic;
        boxCollider2D.enabled = true;
    }

    private void OnDisable()
    {
        anim.SetBool("Explode", false);
    }

    public void InActive()
    {
        transform.gameObject.SetActive(false);
    }


}
