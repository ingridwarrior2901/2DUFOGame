using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyModel Data", menuName = "CurrencyModel")]
public class CurrencyModel : ScriptableObject
{
    [SerializeField]
    private int speedCost;

    [SerializeField]
    private int incomeCost;

    [SerializeField]
    private int spawnRateCost;

    public int SpeedCost
    {
        get
        {
            return speedCost;
        }
    }

    public int IncomeCost
    {
        get
        {
            return incomeCost;
        }
    }

    public int SpawnRateCost
    {
        get
        {
            return spawnRateCost;
        }
    }
}