using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        CoinsManager.instance.OnCoinsChanged += ChangeCoinText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeCoinText(int X)
    {
        coinsText.text = "Coins: " + CoinsManager.instance.Coins;
    }
}
