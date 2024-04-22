using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
