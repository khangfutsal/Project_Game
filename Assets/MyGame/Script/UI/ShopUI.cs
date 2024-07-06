using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textCrystal;

    [SerializeField] private Button btnRandomCard;
    [SerializeField] private int gemRandom;

    [SerializeField] private CardManager cardManager;

    [SerializeField] private List<int> listRandom = new List<int>();


    [SerializeField] public static UnityEvent OnShowSuccess = new UnityEvent();


    private void Start()
    {
        InitializeCard();
        btnRandomCard.onClick.AddListener(ButtonRandom);

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
        var horizontalGroupComponent = cardManager.GetComponentHorizontalGroup();

        var cardsUI = cardManager.GetCardsUI();

        var curCardsUI = DataManager.GetInstance().dataPlayerSO.curCardsUI;
        Debug.Log(curCardsUI.Count + " " + cardsUI.Count);
        for (int i = 0; i < cardsUI.Count; i++)
        {

            if (OnCheckValidCard(curCardsUI[i], cardsUI[i]))
            {
                cardsUI[i].SubEvent(curCardsUI[i]);
            }
            else
            {
                cardsUI[i].gameObject.SetActive(false);
            }

        }


        StartCoroutine(DisableHorizontal(horizontalGroupComponent));

        IEnumerator DisableHorizontal(HorizontalLayoutGroup horizontalGroup)
        {
            yield return new WaitForEndOfFrame();

            horizontalGroup.enabled = false;
        }
    }

    public void CheckBoughtMaxLevel(CardInfo cardInfo)
    {
        var cardsInfo = cardManager.GetCardsInfo();

        if (cardInfo._isBought && cardInfo._maxLevel)
        {
            foreach (var card in cardsInfo)
            {
                if (card.name == cardInfo.name)
                {
                    card._isBought = true;
                }
            }
        }

    }

    public bool IsBoughtMaxLevel()
    {
        var cardsInfo = cardManager.GetCardsInfo();
        if (cardsInfo.FindAll(obj => obj._maxLevel).All
                  (obj => obj._isBought))
        {
            return true;
        }
        return false;
    }
    public void ButtonRandom()
    {
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipClick = AudioController.GetInstance().manager.GetAudioClick();
        AudioController.GetInstance().StartMusic(aClipClick, aSrc);

        RandomCards();


        void RandomCards()
        {
            bool chooseCardInRandom = false;
            var cardsInfo = cardManager.GetCardsInfo();
            var cardsUI = cardManager.GetCardsUI();

            if (IsBoughtMaxLevel())
            {
                foreach (var card in cardsUI)
                {
                    Debug.Log(card.name);
                    card.gameObject.SetActive(false);
                }
                return;
            }
            var curGem = DataManager.GetInstance().dataPlayerSO.curCoin;
            if (curGem >= gemRandom)
            {
                curGem -= gemRandom;
                GameController.GetInstance().gameManager.SetCoin(curGem);
                DataManager.GetInstance().dataPlayerSO.curCoin = curGem;

                var collection_Ins = Collection_Controller.GetInstance();
                collection_Ins.StartCoroutine(collection_Ins.TakeCoin(gemRandom));

                var horizontalGroupComponent = cardManager.GetComponentHorizontalGroup();
                //horizontalGroupComponent.enabled = true;

                listRandom.Clear();
                DataManager.GetInstance().dataPlayerSO.curCardsUI.Clear();

                for (int i = 0; i < cardsUI.Count; i++)
                {
                    chooseCardInRandom = false;
                    while (!chooseCardInRandom)
                    {
                        int random = UnityEngine.Random.Range(0, cardsInfo.Count);
                        if (!listRandom.Contains(random))
                        {
                            listRandom.Add(random);
                            var cardInfo = cardsInfo[random];
                            if (OnCheckValidCard(cardInfo, cardsUI[i]))
                            {
                                chooseCardInRandom = true;
                                DataManager.GetInstance().dataPlayerSO.curCardsUI.Add(cardInfo);

                                cardsUI[i].SubEvent(cardInfo);
                            }
                            if (chooseCardInRandom)
                            {
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
            else return;

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
