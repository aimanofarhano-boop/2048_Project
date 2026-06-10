using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField] 
    public GameObject[] EnemiesInWave { get; private set; }
    [field: SerializeField]
    public float TimeBeforeThisWave { get; private set; }
    [field: SerializeField]
    public float NumberToSpawn { get; private set; }
}
