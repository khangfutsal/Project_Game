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
        textCoin.text = Collection_Controller.GetInstance().GetCollectionManager().GetCoinGem().GetCoin().ToString();
        textCrystal.text = Collection_Controller.GetInstance().GetCollectionManager().GetCrystalGem().GetCrystal().ToString();
    }

    public void InitializeCard()
    {
        var horizontalGroupComponent = cardManager.GetComponentHorizontalGroup();

        var cardsInfo = cardManager.GetCardsInfo();
        var cardsUI = cardManager.GetCardsUI();

        foreach (var cardInfo in cardsInfo)
        {
            cardInfo._isBought = false;
        }
        horizontalGroupComponent.enabled = true;

        for (int i = 0; i < cardsUI.Count; i++)
        {
            OnCheckValidCard(cardsInfo[i], cardsUI[i]);
            cardsUI[i].SubEvent(cardsInfo[i]);
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
        horizontalGroupComponent.enabled = true;

        listRandom.Clear();

        for (int i = 0; i < cardsUI.Count; i++)
        {
            while (true)
            {
                int random = UnityEngine.Random.Range(0, cardsInfo.Count);

                if (!listRandom.Contains(random))
                {
                    listRandom.Add(random);
                    var cardInfo = cardsInfo[random];
                    Debug.Log("Random : " + random);
                    if (OnCheckValidCard(cardInfo, cardsUI[i]))
                    {
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
