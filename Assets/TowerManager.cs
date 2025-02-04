using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance; // Singleton for easy access
    public Color highlightColor = Color.green; // Color for highlighting mergeable towers
    private List<GameObject> mergeableTowers = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Finds all towers that can merge with the selected one
    public void HighlightMergeableTowers(GameObject selectedTower)
    {
        mergeableTowers.Clear();
        Tower towerComponent = selectedTower.GetComponent<Tower>();

        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            if (tower != selectedTower && tower.name == selectedTower.name)
            {
                mergeableTowers.Add(tower);
                SetOutline(tower, true);
            }
        }
    }

    // Removes the highlight from all towers
    public void ClearMergeHighlights()
    {
        foreach (GameObject tower in mergeableTowers)
        {
            SetOutline(tower, false);
        }
        mergeableTowers.Clear();
    }

    // Enables or disables an outline effect on a tower
    private void SetOutline(GameObject tower, bool enable)
    {
        SpriteRenderer spriteRenderer = tower.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            if (enable)
                spriteRenderer.color = highlightColor; // Apply highlight color
            else
                spriteRenderer.color = Color.white; // Reset color
        }
    }
}
