using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractTilemap : MonoBehaviour
{
    [SerializeField] private GameObject endPos;
    [SerializeField] private GameObject startPos;
    [SerializeField] private GameObject player;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float xInverseLerp;



    void Update()
    {
        xInverseLerp = Mathf.InverseLerp(startPos.transform.position.x, endPos.transform.position.x, player.transform.position.x);

        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, xInverseLerp);
    }
}
