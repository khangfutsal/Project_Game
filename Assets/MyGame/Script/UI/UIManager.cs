using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Shop UI")]
    [SerializeField] private GameObject shopUI;

    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI titleWave;

    #region Get Function
    public GameObject GetShopUI() => shopUI;
    public TextMeshProUGUI GetTitleWave() => titleWave;
    #endregion
}
