using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    [SerializeField] private float delay;


    public void Destroy()
    {
        transform.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Invoke("Destroy", delay);
    }


}
