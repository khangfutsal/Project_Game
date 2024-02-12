using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private int currentWave;
    [SerializeField] private int maxWave;
    [SerializeField] private List<int> listRandom;

    [SerializeField] private bool _isInitialize;

    [SerializeField] private List<Transform> traps;
    [SerializeField] private Transform enemiesHolder;
    [SerializeField] private BoxCollider2D boxCollider2D;

    public UnityEvent a;


    public UnityEvent onSuccessChapter1 = new UnityEvent();
    public UnityEvent onSuccessChapter2 = new UnityEvent();

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SetupScene();
    }

    public void SetupScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Chap 1")
        {
            Initialize();
        }
        if (currentScene.name == "Chap 2")
        {
            PlayWave();
        }
    }



    public void Initialize()
    {
        StartCoroutine(CheckStatusEnemies());

        IEnumerator CheckStatusEnemies()
        {
            StartCoroutine(SetListEnmies());
            yield return new WaitUntil(() => _isInitialize && enemiesHolder.childCount == 0);
            onSuccessChapter1?.Invoke();
        }

        IEnumerator SetListEnmies()
        {
            yield return new WaitUntil(() => enemiesHolder.childCount > 0);
            _isInitialize = true;
        }
    }


    public void PlayWave()
    {

        StartCoroutine(SetWaves());

        IEnumerator SetWaves()
        {
            StartCoroutine(WaveUI((currentWave + 1).ToString()));
            yield return new WaitForSeconds(3f);

            traps.Clear();

            _isInitialize = false;
            while (true)
            {
                var Waves = waveManager.GetWaves();
                int rand = UnityEngine.Random.Range(0, Waves.Count);
                if (!listRandom.Contains(rand))
                {
                    listRandom.Add(rand);

                    if (!Waves[rand]._isSpawn)
                    {
                        foreach (var waveData in Waves[rand].WavesData)
                        {
                            foreach (var trap in waveData.traps)
                            {
                                float timedelay = trap.timeDelay;
                                GameObject trapobj = Instantiate(trap.trapObj, trap.spawnPos.transform.position, Quaternion.Euler(trap.spawnPos.transform.localEulerAngles));

                                trapobj.GetComponentInChildren<Trap>().data.timeDelay = timedelay;
                                traps.Add(trapobj.transform);
                            }
                            foreach (var enemy in waveData.enemies)
                            {
                                GameObject objEnemy = Instantiate(enemy.enemyObj, enemy.spawnObj.transform.position, Quaternion.identity, enemiesHolder);
                            }
                        }
                        StartCoroutine(SetListEnmies());
                        Waves[rand]._isSpawn = true;
                        currentWave++;
                        break;
                    }
                }
            }


            yield return new WaitUntil(() => _isInitialize && enemiesHolder.childCount == 0);

            yield return new WaitForSeconds(3f);
            ClearWave();


            if (currentWave < maxWave)
            {
                Debug.Log("curWave");
                StartCoroutine(SetWaves());
                yield return null;
            }
            else
            {
                Debug.Log("Done Waves");
                onSuccessChapter2?.Invoke();
                yield return null;
            }
        }

        IEnumerator SetListEnmies()
        {
            yield return new WaitUntil(() => enemiesHolder.childCount > 0);
            _isInitialize = true;
        }

        void ClearWave()
        {
            foreach (var trap in traps)
            {
                Destroy(trap.gameObject);
            }
        }

        IEnumerator WaveUI(string curWave)
        {
            var textTitleWave = UIController.GetInstance().uiManager.GetTitleWave();
            textTitleWave.gameObject.SetActive(true);
            textTitleWave.text = "ROUND " + curWave;

            float alpha = 0;
            while (alpha <= 1)
            {
                alpha += Time.deltaTime * 0.5f;
                textTitleWave.color = new Color(textTitleWave.color.r, textTitleWave.color.g, textTitleWave.color.b, alpha);
                yield return null; 
            }

            while (alpha >= 0)
            {
                alpha -= Time.deltaTime * 0.5f; 
                textTitleWave.color = new Color(textTitleWave.color.r, textTitleWave.color.g, textTitleWave.color.b, alpha);
                yield return null; 
            }

            textTitleWave.gameObject.SetActive(false); 
        }

        

    }


}
