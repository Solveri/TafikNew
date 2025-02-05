using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    public bool isOccupid;
    public Tower ocuupier;

    public static event System.Action<Plot> OnPlotClicked;
   



    // Start is called before the first frame update
    void Start()
    {
        isOccupid = false;
    }
    private void OnMouseDown()
    {
        Debug.Log("This has been clicked");
        OnPlotClicked?.Invoke(this);
        //need to move it to the SelectionManager
        if (!isOccupid)
        {
            if(CoinsManager.instance.Coins >= 30)
            {
                isOccupid = true;
                Instantiate(SelectionManager.Instance.SelectedTower, transform.position, Quaternion.identity).transform.SetParent(this.transform);
                CoinsManager.instance.RemoveCoins(30);

            }
            else
            {
                Debug.Log("Doesn't have enough money");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
