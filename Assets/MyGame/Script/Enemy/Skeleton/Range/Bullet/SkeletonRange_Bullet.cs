using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange_Bullet : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rgbody2D;

    [Header("Properties Object")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float force;
    [SerializeField] private float radiusOfPosition;


    [SerializeField] private GameObject test_vfx;
    [SerializeField] private List<Vector3> listPositionCheck;

    [SerializeField] private Skeleton_Range skeleton_Range;
    [SerializeField] private GameObject skeleton_RangeObj;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rgbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        skeleton_Range = skeleton_RangeObj.GetComponentInChildren<Skeleton_Range>();
    }




    public void FireBullet(Transform hitboxTf)
    {
        //AimToPlayer();
        AimPlayer();

        void AimPlayer()
        {
            float rotation = -50;
            bool check = false;

            transform.position = hitboxTf.position;

            do
            {
                bool validPositionFound;
                while (rotation < 120)
                {
                    hitboxTf.localEulerAngles = new Vector3(0, 0, rotation);

                    listPositionCheck.Clear();

                    validPositionFound = false;
                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 newVector = PointPosition((listPositionCheck.Count) * 0.1f);

                        bool checkValidPosition = CheckPosition(newVector);
                        if (!checkValidPosition)
                        {
                            listPositionCheck.Add(newVector);
                            validPositionFound = true;
                        }
                        else
                        {
                            Shoot();
                            check = true;
                            break;
                        }
                    }

                    if (check) break;

                    rotation++;
                }
                if (!check)
                {
                    ++force;
                    rotation = -35;
                }


            } while (!check);
        }


        Vector2 PointPosition(float t)
        {
            Vector2 currentPosition = (Vector2)transform.position + ((Vector2)hitboxTf.right.normalized * force * t) + .5f *
                Physics2D.gravity * (t * t);
            return currentPosition;
        }

        void Shoot()
        {
            rgbody2D.velocity = hitboxTf.right * force;
        }


        bool CheckPosition(Vector2 v)
        {
            Collider2D collider2D = Physics2D.OverlapCircle(v, radiusOfPosition, playerMask);
            if (collider2D != null)
            {
                return true;
            }
            return false;
        }
    }

    public IEnumerator DestroyObj()
    {
        boxCollider2D.enabled = false;
        rgbody2D.bodyType = RigidbodyType2D.Static;
        animator.SetBool("destroy", true);
        yield return new WaitForSeconds(2);
        transform.gameObject.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DestroyObj());
            IDmgable damageable = collision.GetComponent<IDmgable>();


            float dmg = skeleton_Range.GetFloat_DmgAttack();

            if (damageable != null)
            {
                damageable.TakeDamage(dmg, transform);
            }
        }
        else if (collision.CompareTag("Grounded"))
        {
            StartCoroutine(DestroyObj());
        }
    }



    void OnEnable()
    {
        force = 8;
        boxCollider2D.enabled = true;
        rgbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnDisable()
    {
        animator.SetBool("destroy", false);
    }
}
