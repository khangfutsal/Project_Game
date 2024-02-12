using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [Header("Holder Gem")]
    [SerializeField] private Transform coinHolderTf;
    [SerializeField] private Transform crystalHolderTf;

    [Header("Object Reference")]
    [SerializeField] private CoinGem coin;
    [SerializeField] private CrystalGem crystal;

    [Header("Properties Collection")]
    [SerializeField] private int minGem;
    [SerializeField] private int maxGem;

    #region Get Function
    public Transform GetCoinHolderTf() => coinHolderTf;
    public Transform GetCrystalHolderTf() => crystalHolderTf;

    public CoinGem GetCoinGem() => coin;
    public CrystalGem GetCrystalGem() => crystal;

    public int GetMinGem() => minGem;
    public int GetMaxGem() => maxGem;
    #endregion

    #region Set Function
    public void SetMinGem(int _minGem) => minGem = _minGem;
    public void SetMaxGem(int _maxGem) => maxGem = _maxGem;
    #endregion


}
