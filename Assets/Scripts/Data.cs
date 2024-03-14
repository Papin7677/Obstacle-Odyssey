using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Data
{
    public int health;
    public int coins;

    public Data(CoinManager coinManager)
    {
        this.coins = coinManager.coinNum;
    }

}
