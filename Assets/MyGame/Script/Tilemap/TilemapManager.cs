using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tiles;

    public List<GameObject> GetTiles() => tiles;

    public void ShowDefaultMap()
    {
        tiles[0].SetActive(true);
        tiles[1].SetActive(false);
    }

    public void ShowFinalMap()
    {
        tiles[1].SetActive(true);
        tiles[0].SetActive(false);
    }
}
