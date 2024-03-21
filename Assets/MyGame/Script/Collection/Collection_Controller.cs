using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_Controller : MonoBehaviour
{
    [Header("Object Reference")]
    private static Collection_Controller _ins;

    [SerializeField] private CollectionManager collectionManager;
    [SerializeField] private TypeCollection curCollection;

    [SerializeField] private float coinPercent;
    [SerializeField] private float crystalPercent;

    public bool _coinCou;
    public bool _crystalCou;

    public enum TypeCollection
    {
        Coin,
        Crystal
    }

    #region Get Function
    public CollectionManager GetCollectionManager() => collectionManager;
    public static Collection_Controller GetInstance() => _ins;
    #endregion

    private void Awake()
    {
        _ins = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnGem(transform);
        }
    }

    public void SpawnGem(Transform tf)
    {
        int minGem = collectionManager.GetMinGem();
        int maxGem = collectionManager.GetMaxGem();

        CoinGem coin = collectionManager.GetCoinGem();
        CrystalGem crystal = collectionManager.GetCrystalGem();

        string gemStrRandom;
        for (int i = minGem; i < maxGem; i++)
        {
            curCollection = RandomGem();
            switch (curCollection)
            {
                case TypeCollection.Coin:
                    {
                        GameObject obj = Instantiate(coin.gameObject, tf.position, Quaternion.identity);
                        RandomVelocity(obj.transform);
                        break;
                    }
                case TypeCollection.Crystal:
                    {
                        GameObject obj = Instantiate(crystal.gameObject, tf.position, Quaternion.identity);
                        RandomVelocity(obj.transform);
                        break;
                    }
            }
        }



        #region Local Function
        void RandomVelocity(Transform tf)
        {
            float xRandom = UnityEngine.Random.Range(-3, 3);
            float yRandom = UnityEngine.Random.Range(3, 10);

            Rigidbody2D rg2D = tf.GetComponent<Rigidbody2D>();
            rg2D.bodyType = RigidbodyType2D.Dynamic;
            rg2D.velocity = new Vector2(xRandom, yRandom);
        }

        TypeCollection RandomGem()
        {
            TypeCollection gem;
            float random = UnityEngine.Random.Range(0, 10);
            if (random < crystalPercent)
            {
                return TypeCollection.Crystal;
            }
            else return TypeCollection.Coin;
        }
        #endregion
    }

    public IEnumerator TakeCrystal(float price)
    {
        yield return new WaitUntil(() => !_crystalCou);

        var crystal = GameController.GetInstance().gameManager.GetCrystalUI();

        var minuscrystal = crystal - (int)price;

        StartCoroutine(Countdown(crystal, minuscrystal, "Crystal"));



    }

    public IEnumerator TakeCoin(float price)
    {

        yield return new WaitUntil(() => !_coinCou);

        var coin = GameController.GetInstance().gameManager.GetCoinUI();

        var minusCoin = coin - (int)price;

        Debug.Log(coin);


        StartCoroutine(Countdown(coin, minusCoin, "Coin"));

    }

    public IEnumerator Countdown(int curGem, int endGem, string nameGem)
    {
        while (curGem != endGem)
        {
            switch (nameGem)
            {
                case "Coin":
                    {

                        GameController.GetInstance().gameManager.SetCoinUI(--curGem);
                        DataManager.GetInstance().dataPlayerSO.curCoin = curGem;

                        _coinCou = true;
                        break;
                    }
                case "Crystal":
                    {

                        GameController.GetInstance().gameManager.SetCrystalUI(--curGem);
                        DataManager.GetInstance().dataPlayerSO.curCrystal = curGem;

                        _crystalCou = true;
                        break;
                    }
            }

            //curGem--;
            yield return null;
        }
        ResetCouroutine(nameGem);

        void ResetCouroutine(string nameGem)
        {
            if (nameGem == "Coin")
            {
                _coinCou = false;
            }
            if (nameGem == "Crystal")
            {
                _crystalCou = false;
            }
        }

    }


}
