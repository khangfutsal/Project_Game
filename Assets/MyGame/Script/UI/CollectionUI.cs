using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUI : MonoBehaviour
{
    [Header("Group Coin")]
    [SerializeField] private GameObject groupCoinUI;
    [SerializeField] private Image signCoin;
    [SerializeField] private TextMeshProUGUI textCoin;

    [Space()]
    [Header("Group Crystal")]
    [SerializeField] private GameObject groupCrystalUI;
    [SerializeField] private Image signCrystal;
    [SerializeField] private TextMeshProUGUI textCrystal;


    private Coroutine IEInActiveCoin;
    private Coroutine IEInActiveCrystal;

    private static CollectionUI _ins;
    #region Get Function
    public static CollectionUI GetInstance() => _ins;
    #endregion
    private void Awake()
    {
        _ins = this;
    }


    private void Update()
    {
        UpdateGem();
    }


    public void ShowGroupCrystalUI()
    {
        groupCrystalUI.SetActive(true);

        textCrystal.text = GameController.GetInstance().gameManager.GetCrystalUI().ToString();

        if (IEInActiveCrystal != null)
        {
            StopCoroutine(IEInActiveCrystal);
            IEInActiveCrystal = StartCoroutine(InActive("Crystal"));
        }
        else
        {
            IEInActiveCrystal = StartCoroutine(InActive("Crystal"));
        }
    }

    public void ShowGroupCoinUI()
    {
        groupCoinUI.SetActive(true);
        
        textCoin.text = GameController.GetInstance().gameManager.GetCoinUI().ToString();

        if (IEInActiveCoin != null)
        {
            StopCoroutine(IEInActiveCoin);
            IEInActiveCoin = StartCoroutine(InActive("Coin"));
        }
        else
        {
            IEInActiveCoin = StartCoroutine(InActive("Coin"));
        }
    }

    public void UpdateGem()
    {
        if (!groupCoinUI.activeSelf) return;
        textCoin.text = GameController.GetInstance().gameManager.GetCoinUI().ToString();

        if (!groupCrystalUI.activeSelf) return;
        textCrystal.text = GameController.GetInstance().gameManager.GetCrystalUI().ToString();
    }

    public IEnumerator InActive(string name)
    {
        yield return new WaitForSeconds(1f);
        switch (name)
        {
            case "Coin":
                {
                    groupCoinUI.SetActive(false);
                    break;
                }
            case "Crystal":
                {
                    groupCrystalUI.SetActive(false);
                    break;
                }
        }
    }
}
