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

    [SerializeField] private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgbody2D = GetComponent<Rigidbody2D>();
    }

   



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || (collision.CompareTag("Wall") && _interactWall) || (collision.CompareTag("Grounded") && _interactGrounded))
        {
            anim.SetBool("Explode", true);
            InActive();
            rgbody2D.bodyType = RigidbodyType2D.Static;
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
        Invoke("InActive", timeDestroy);
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
