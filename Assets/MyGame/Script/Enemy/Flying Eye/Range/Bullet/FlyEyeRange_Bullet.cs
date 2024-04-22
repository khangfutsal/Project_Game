using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyEyeRange_Bullet : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private Transform playerTf;

    [Header("Properties Object")]
    [SerializeField] private float speed;
    [SerializeField] private int currentTimes;
    [SerializeField] private int times;

    [SerializeField] private GameObject test_vfx;

    [SerializeField] private FlyingEye_Range flyingEye_Range;

    public bool _canAttack;
    public Tween tween;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerTf = GameObject.Find("BonzePlayer").transform;
        flyingEye_Range = transform.parent.parent.GetComponentInChildren<FlyingEye_Range>();
    }


    private void Update()
    {
        if (currentTimes >= times)
        {
            StartCoroutine(DestroyObj());
        }
    }

    public void StopFireBullet()
    {
        tween.Kill();
    }

    public void FireBullet()
    {
        tween = transform.DOScale(new Vector2(1.5f, 1.5f), 5f).OnComplete(() =>
        {
            Debug.Log("Fire");
            flyingEye_Range.SetBool_IsFired(true);

            StartCoroutine(FireBulletTarget(playerTf.position));
        });
        


        IEnumerator FireBulletTarget(Vector3 targetTf)
        {
            boxCollider2D.enabled = true;

            Vector3 targetPosition = targetTf;
            while (Vector3.Distance(transform.position, targetPosition) > .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            StartCoroutine(RotateObjectCoroutine());


        }

        IEnumerator RotateObjectCoroutine()
        {
            float time = 0;
            float rotationSpeed = 10000f;
            if (currentTimes < times)
            {
                while (time < 5f)
                {
                    transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                    time += Time.deltaTime * 2f;
                    yield return null;
                }
                Vector3 bulletToPlayer = transform.position + (playerTf.position - transform.position).normalized * 10f;
                StartCoroutine(FireBulletTarget(bulletToPlayer));
                currentTimes++;
            }
        }

    }

    public IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(2);
        transform.gameObject.SetActive(false);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.gameObject.SetActive(false);
            IDmgable damageable = collision.GetComponent<IDmgable>();
            

            float dmg = flyingEye_Range.GetFloat_DmgAttack();
            if (damageable != null)
            {
                damageable.TakeDamage(dmg, transform);
            }
        }
    }

    void OnEnable()
    {
        boxCollider2D.enabled = false;
        transform.localScale = new Vector3(0, 0, 0);
        currentTimes = 0;
    }

}
