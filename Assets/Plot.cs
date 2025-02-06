using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public bool isOccupied;
    public Tower occupier;

    public static event System.Action<Plot> OnPlotClicked;

    private void Start()
    {
        isOccupied = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("This plot has been clicked");
        OnPlotClicked?.Invoke(this);

        // Place a tower on click if it's empty and player has enough coins
        if (!isOccupied && CoinsManager.instance.Coins >= 30)
        {
            PlaceTower(SelectionManager.Instance.SelectedTower);
            CoinsManager.instance.RemoveCoins(30);
        }
        else if (isOccupied)
        {
            Debug.Log("Plot is already occupied");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void PlaceTower(Tower towerPrefab)
    {
        if (isOccupied) return; // Prevent placing multiple towers

        isOccupied = true;
        Tower newTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        newTower.transform.SetParent(this.transform);
        occupier = newTower;
    }
}
