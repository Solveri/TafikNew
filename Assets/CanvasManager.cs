using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField]List<Image> Hearts = new List<Image>();
   
    // Start is called before the first frame update
    void Start()
    {
        CoinsManager.instance.OnCoinsChanged += ChangeCoinText;
        PlayerManager.instance.OnPlayerHit += DisableHeartOnHit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeCoinText(int X)
    {
        coinsText.text = "Coins: " + CoinsManager.instance.Coins;
    }
    void DisableHeartOnHit(int index)
    {
        if (index <0)
        {
            return;
        }
        
        Image heart = Hearts[index];
        Debug.Log(index);

        if (heart != null)
        {
            heart.enabled = false;
        }
        
       

    }
}
