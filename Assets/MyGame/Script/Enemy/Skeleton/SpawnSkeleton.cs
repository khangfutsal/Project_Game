using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : MonoBehaviour
{
    [SerializeField] private GameObject skeletonObj;

    [SerializeField] private Transform pointSpawn;
    [SerializeField] private Transform enemyTf;

    [SerializeField] private Vector2 offset;
    private bool _isHavePosition;

    private void Awake()
    {
        enemyTf = GameObject.Find("Enemy").transform;
    }
    private void Update()
    {
        if (_isHavePosition) return;
        SetGroundCracks();
    }
    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2);
        Instantiate(skeletonObj, pointSpawn.position, Quaternion.identity,enemyTf);
        Destroy(transform.gameObject);
    }

    public void SetGroundCracks()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Grounded"))
            {
                transform.position = hit.point - offset;
                _isHavePosition = true;
                StartCoroutine(SpawnEnemy());
            }
        }
    }
}
