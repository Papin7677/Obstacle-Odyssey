using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinNum;
    public TextMeshProUGUI displayText;
    void Start()
    {
        if (SaveSystem.LoadData() != null)
        {
            coinNum = SaveSystem.LoadData().coins;
        }
        displayText.text = coinNum.ToString();

    }

    // Update is called once per frame

    public void addCoin(int num)
    {
        coinNum = num + coinNum;
        displayText.text = coinNum.ToString();

    }
    public void removeCoin(int num)
    {
        coinNum = coinNum - num;
        displayText.text = coinNum.ToString();
    }
}
