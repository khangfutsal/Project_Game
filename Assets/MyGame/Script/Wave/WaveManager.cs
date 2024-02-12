using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WavePattern> Waves;

    #region Get Function    
    public List<WavePattern> GetWaves() => Waves;
    #endregion

    #region Set Function

    #endregion

}
[Serializable]
public class WavePattern
{
    public bool _isSpawn;
    public List<WaveScriptObj> WavesData;
}
