using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Wave",menuName ="ScriptableObj/Enemy/WaveEnemy")]
public class WaveScriptObj : ScriptableObject
{
    [SerializeField] public List<EnemyPattern> enemies;

    [SerializeField] public List<TrapPatern> traps;
}
