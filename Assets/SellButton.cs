using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button button;
  
    void Start()
    {
        button.onClick.AddListener(SellTower);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SellTower()
    {
       if (SelectionManager.Instance.SelectedTower != null)
        {
        CoinsManager.instance.SellTower(SelectionManager.Instance.SelectedTower);

        }
    }
}
