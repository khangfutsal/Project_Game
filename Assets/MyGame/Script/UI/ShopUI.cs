using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textCrystal;

    [SerializeField] private Button btnRandomCard;
    [SerializeField] private CardManager cardManager;

    [SerializeField] private List<int> listRandom = new List<int>();


    private static ShopUI _ins;

    public static ShopUI GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        InitializeCard();
        btnRandomCard.onClick.AddListener(Random);

    }

    private void Update()
    {
        UpdateGem();
    }

    public void UpdateGem()
    {
        if (!transform.gameObject.activeSelf) return;
        textCoin.text = GameController.GetInstance().gameManager.GetCoinUI().ToString();
        textCrystal.text = GameController.GetInstance().gameManager.GetCrystalUI().ToString();
    }

    public void InitializeCard()
    {
        bool BoughtFullInitializeCard = false;
        var horizontalGroupComponent = cardManager.GetComponentHorizontalGroup();

        var cardsUI = cardManager.GetCardsUI();

        var curCardsUI = DataManager.GetInstance().dataPlayerSO.curCardsUI;
        for (int i = 0; i < cardsUI.Count; i++)
        {
            BoughtFullInitializeCard = false;

            if (OnCheckValidCard(curCardsUI[i], cardsUI[i]))
            {
                cardsUI[i].SubEvent(curCardsUI[i]);
            }
            else
            {
                BoughtFullInitializeCard = true;
                cardsUI[i].gameObject.SetActive(false);
            }

        }

        if (BoughtFullInitializeCard)
        {
            var curCardsInData = DataManager.GetInstance().dataPlayerSO.curCardsUI;

            for (int i = 0; i < curCardsInData.Count; i++)
            {
                if (OnCheckValidCard(curCardsInData[i], cardsUI[i]))
                {
                    cardsUI[i].SubEvent(curCardsInData[i]);
                }
                else
                {
                    cardsUI[i].gameObject.SetActive(false);
                }
            }
        }


        StartCoroutine(DisableHorizontal(horizontalGroupComponent));

        IEnumerator DisableHorizontal(HorizontalLayoutGroup horizontalGroup)
        {
            yield return new WaitForEndOfFrame();

            horizontalGroup.enabled = false;
        }
    }

    public void CheckStatusCardInfo(CardInfo cardInfo)
    {
        var cardsInfo = cardManager.GetCardsInfo();

        if (cardInfo._maxLevel && cardInfo._isBought)
        {
            string cardName = cardInfo.name;

            foreach (var ele in cardsInfo)
            {
                if (ele.name == cardName)
                {

                    DataManager.GetInstance().dataPlayerSO.listCards.ForEach(cardDataSO =>
                    {
                        if (cardDataSO.name == ele.name)
                        {
                            cardDataSO._isBought = true;
                        }
                    });

                    ele._isBought = true;
                }
            }
        }


    }

    public void Random()
    {

        var cardsInfo = cardManager.GetCardsInfo();
        var cardsUI = cardManager.GetCardsUI();

        if (cardsInfo.FindAll(obj => obj._maxLevel).
              All(obj => obj._isBought))
        {
            foreach (var card in cardsUI)
            {
                Debug.Log(card.name);
                card.gameObject.SetActive(false);
            }
            return;
        }


        var horizontalGroupComponent = cardManager.GetComponentHorizontalGroup();
        //horizontalGroupComponent.enabled = true;

        listRandom.Clear();
        DataManager.GetInstance().dataPlayerSO.curCardsUI.Clear();

        for (int i = 0; i < cardsUI.Count; i++)
        {
            while (true)
            {
                int random = UnityEngine.Random.Range(0, cardsInfo.Count);

                if (!listRandom.Contains(random))
                {
                    listRandom.Add(random);
                    var cardInfo = cardsInfo[random];
                    Debug.Log("Random : " + cardInfo.name);
                    if (OnCheckValidCard(cardInfo, cardsUI[i]))
                    {

                        DataManager.GetInstance().dataPlayerSO.curCardsUI.Add(cardInfo);

                        cardsUI[i].SubEvent(cardInfo);
                        break;
                    }
                }

            }
        }

        StartCoroutine(DisableHorizontal(horizontalGroupComponent));

        IEnumerator DisableHorizontal(HorizontalLayoutGroup horizontalGroup)
        {
            yield return new WaitForEndOfFrame();

            horizontalGroup.enabled = false;
        }
    }

    public bool OnCheckValidCard(CardInfo cardInfo, CardUI cardUI)
    {
        if (!cardInfo._isBought)
        {

            cardUI.gameObject.SetActive(true);

            cardUI.dataCard.Clear();
            cardUI.name = cardInfo.name;
            Debug.Log("Random : " + cardUI.name);
            cardUI.price.text = cardInfo.price.ToString();
            cardUI.textDescription.text = cardInfo.description;

            cardUI.imgStamina.sprite = cardInfo.imgStamina.GetComponent<SpriteRenderer>().sprite;
            cardUI.imgStamina.SetNativeSize();

            for (int j = 0; j < cardInfo.dataCard.Count; j++)
            {
                cardUI.dataCard.Add(cardInfo.dataCard[j]);
            }

            cardUI.imgCollection.sprite = cardInfo.imgCollection.GetComponent<SpriteRenderer>().sprite;
            RectTransform rectransform = cardUI.imgCollection.GetComponent<RectTransform>();

            float width;
            float height;
            if (cardInfo.imgCollection.gameObject.name == "Crystal")
            {
                width = 46;
                height = width + 23;
                rectransform.sizeDelta = new Vector2(width, height);
            }
            else if (cardInfo.imgCollection.gameObject.name == "Coin")
            {
                width = 100;
                height = 100;
                rectransform.sizeDelta = new Vector2(width, height);
            }
            return true;
        }
        return false;
    }

}
