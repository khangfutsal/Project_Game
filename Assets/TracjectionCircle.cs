using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracjectionCircle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform playerTf;

    //private void Update()
    //{
    //    CheckPosition();
    //    //Debug.Log(Vector3.Distance(transform.transform.position, playerTf.position));
        
    //}

    public bool CheckPosition()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collider2D != null)
        {
            return true;
        }
        return false;
    }
}
