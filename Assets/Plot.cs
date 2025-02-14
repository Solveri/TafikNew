using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public bool isOccupied;
    public Tower occupier;

    public SpriteRenderer spriteRenderer;

    public static event System.Action<Plot> OnPlotClicked;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOccupied = false;
    }
    private void Start()
    {
      SelectionManager.Instance.SelectionChanged += OnPlotClicked;
    }
    private void Update()
    {
        if (!isOccupied)
        {
            if (LevelManager.instance.isHoldingImage)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
        
    }
    private void OnMouseDown()
    {
        Debug.Log("This plot has been clicked");
        OnPlotClicked?.Invoke(this);
        if (isOccupied)
        {
            Debug.Log("Plot is already occupied");
        }
       
    }
    public void RestPlot()
    {
        isOccupied = false;
        occupier = null;
    }
    public Tower PlaceTower(Tower towerPrefab)
    {
        spriteRenderer.enabled = true;
        isOccupied = true;
        Tower newTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        newTower.transform.SetParent(this.transform);
        occupier = newTower;
        return newTower;
    }
}
