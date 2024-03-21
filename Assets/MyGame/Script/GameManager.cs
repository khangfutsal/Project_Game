using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int curCoin;
    [SerializeField] public static int curCrystal;

    public int curCoinUI;
    public int curCrystalUI;

    #region Get Function
    public int GetCoinUI() => curCoinUI;
    public int GetCrystalUI() => curCrystalUI;
    public int GetCoin() => curCoin;
    public int GetCrystal() => curCrystal;
    #endregion

    #region Set Function
    public void SetCoinUI(int _coin) => curCoinUI = _coin;
    public void SetCoin(int _coin) => curCoin = _coin;
    public void SetCrystal(int _crystal) => curCrystal = _crystal;
    public void SetCrystalUI(int _crystal) => curCrystalUI = _crystal;
    #endregion


    private void Start()
    {

        
        curCoin = DataManager.GetInstance().dataPlayerSO.curCoin;
        curCrystal = DataManager.GetInstance().dataPlayerSO.curCrystal;

        curCoinUI = curCoin;
        curCrystalUI = curCrystal;

        DontDestroyOnLoad(this);
    }
}
