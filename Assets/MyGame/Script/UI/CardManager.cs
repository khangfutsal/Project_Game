using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<CardInfo> cardsInfo = new List<CardInfo>();
    [SerializeField] private List<CardUI> cardsUI;

    #region Variable Component
    private HorizontalLayoutGroup horizonGroup;
    #endregion

    #region Get Function
    public HorizontalLayoutGroup GetComponentHorizontalGroup() => horizonGroup;
    public List<CardUI> GetCardsUI() => cardsUI;
    public List<CardInfo> GetCardsInfo() => cardsInfo;
    #endregion

    private void Awake()
    {
        horizonGroup = GetComponent<HorizontalLayoutGroup>();
    }
}



