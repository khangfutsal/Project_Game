using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveScriptObj> Waves;

    #region Get Function
    public List<WaveScriptObj> GetWaves() => Waves;
    #endregion

    #region Set Function

    #endregion

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Test();
        }
    }

    public void Test()
    {
        foreach (var wave in Waves)
        {
            foreach (var trap in wave.traps)
            {
                float timeDelay = trap.timeDelay;
                GameObject trapObj = Instantiate(trap.trapObj, trap.spawnPos.transform.position, Quaternion.Euler(trap.spawnPos.transform.localEulerAngles));

                trapObj.GetComponentInChildren<Trap>().data.timeDelay = timeDelay;
            }
            foreach(var enemy in wave.enemies)
            {
                GameObject trapObj = Instantiate(enemy.enemyObj, enemy.spawnObj.transform.position, Quaternion.identity);
            }
        }
    }

}
