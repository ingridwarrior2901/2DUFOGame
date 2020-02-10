using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gold Data", menuName = "Gold Data")]
public class GoldNugget : ScriptableObject
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int price;

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }
}
