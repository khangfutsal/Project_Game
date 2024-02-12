using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject endPos;
    [SerializeField] private GameObject player;


    private void Awake()
    {
        player = GameObject.Find("BonzePlayer");
    }

    public void Tele()
    {
        player.transform.position = endPos.transform.position;
    }
}
