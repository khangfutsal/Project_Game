using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public Image imgStamina;
    [SerializeField] public TextMeshProUGUI textDescription;
    [SerializeField] public Image imgCollection;
    [SerializeField] public TextMeshProUGUI price;
    [SerializeField] public List<float> dataCard = new List<float>();

    [SerializeField] public Button btnBuy;

    [SerializeField] private PlayerStats playerStats;

    public void SubEvent(CardInfo cardInfo)
    {
        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() => ButtonBuy(cardInfo));
    }

    private void ButtonBuy(CardInfo cardInfo)
    {
        UpdateCollectionGem(cardInfo);
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipBuy = AudioController.GetInstance().manager.GetAudioBuy();
        AudioController.GetInstance().StartMusic(aClipBuy, aSrc);

        void UpdateDataForPlayer()

        {
            switch (name)
            {
                case "Damage":
                    {

                        int playerStatsDamage = (int)dataCard[0];
                        playerStats.SetInt_AttackDmg(playerStatsDamage);

                        DataManager.GetInstance().dataPlayerSO.curDamage = playerStatsDamage;
                        break;
                    }
                case "Heart":
                    {
                        playerStats.SetFloat_PercentRegenerationHealth(dataCard[0]);
                        playerStats.timeRegenerationHealth = dataCard[1];

                        DataManager.GetInstance().dataPlayerSO.percentRegenerationHealth = (int)dataCard[0];
                        DataManager.GetInstance().dataPlayerSO.timeRegenerationHealth = (int)dataCard[1];
                        break;
                    }
                case "Mana":
                    {
                        playerStats.statsRegenerationMana = dataCard[0];
                        playerStats.timeRegenerationMana = dataCard[1];

                        DataManager.GetInstance().dataPlayerSO.statsRegenerationMana = dataCard[0];
                        DataManager.GetInstance().dataPlayerSO.timeRegenerationMana = dataCard[1];
                        break;
                    }
                case "SkillDefense":
                    {

                        float curSkillStatus = dataCard[0];
                        playerStats.SetFloat_StatusDefense(curSkillStatus);

                        DataManager.GetInstance().dataPlayerSO.statusDefenseSkill = curSkillStatus;
                        break;
                    }
                case "SkillFireball":
                    {
                        float curSkillStatus = dataCard[0];
                        playerStats.SetFloat_StatusFireBall(curSkillStatus);

                        DataManager.GetInstance().dataPlayerSO.statusFireballSkill = curSkillStatus;
                        break;
                    }
                case "SkillEarthquake":
                    {
                        float curSkillStatus = dataCard[0];
                        playerStats.SetFloat_StatusEarthquake(curSkillStatus);

                        DataManager.GetInstance().dataPlayerSO.statusEarthquakeSkill = curSkillStatus;
                        break;
                    }
                default: break;
            }


        }

        void UpdateCollectionGem(CardInfo card)
        {


            float.TryParse(price.text, out float priceValue);
            var typeGem = imgCollection.GetComponent<Image>().sprite.name;
            Debug.Log(typeGem);
            switch (typeGem)
            {
                case "Coin":
                    {
                        var curCoin = GameController.GetInstance().gameManager.GetCoin();
                        

                        if ((float)curCoin >= priceValue)
                        {
                            var remainCoin = curCoin - (int)priceValue;
                            Debug.Log(remainCoin);
                            GameController.GetInstance().gameManager.SetCoin(remainCoin);
                            DataManager.GetInstance().dataPlayerSO.curCoin = remainCoin;

                            var collection_Ins = Collection_Controller.GetInstance();
                            collection_Ins.StartCoroutine(collection_Ins.TakeCoin(priceValue));

                            card._isBought = true;

                            UIController.GetInstance().uiManager.GetShopUI().GetComponent<ShopUI>().CheckBoughtMaxLevel(card);

                            UpdateDataForPlayer();

                            transform.gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Not enough price");
                            return;
                        }
                        break;
                    }
                case "red_crystal_0001_0":
                    {
                        var curCrystal = GameController.GetInstance().gameManager.GetCrystal();
                        if ((float)curCrystal >= priceValue)
                        {
                            var remainCrystal = curCrystal - (int)priceValue;
                            GameController.GetInstance().gameManager.SetCrystal(remainCrystal);
                            DataManager.GetInstance().dataPlayerSO.curCrystal = remainCrystal;

                            var collection_Ins = Collection_Controller.GetInstance();
                            collection_Ins.StartCoroutine(collection_Ins.TakeCrystal(priceValue));

                            var guideSkillUI = UIController.GetInstance().uiManager.GetGuideSKillUI().GetComponent<GuideSkillUI>();
                            guideSkillUI.gameObject.SetActive(true);
                            guideSkillUI.StartCoroutine(guideSkillUI.ShowGuideUI(name));

                            UIController.GetInstance().uiManager.GetShopUI().GetComponent<ShopUI>().CheckBoughtMaxLevel(card);

                            UpdateDataForPlayer();
                            card._isBought = true;

                            transform.gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Not enough price");
                            return;
                        }
                        break;
                    }
            }
        }
    }





}
