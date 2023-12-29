using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeRange_Bullet : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Transform playerTf;

    [Header("Properties Object")]
    [SerializeField] private float speed;
    [SerializeField] private int currentTimes;
    [SerializeField] private int times;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerTf = GameObject.Find("BonzePlayer").transform;
    }


    private void Update()
    {
        if (currentTimes >= times)
        {
            StartCoroutine(DestroyObj());
        }

    }

    public void FireBullet()
    {

        StartCoroutine(FireBulletTarget(playerTf.position));


        IEnumerator FireBulletTarget(Vector3 targetTf)
        {
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
        Debug.Log("collision " + collision.transform.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("A");
        }
    }

}
