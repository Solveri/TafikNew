using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public Material highlightMaterial; // Assign in Inspector
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    public Sprite sprite;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void HighlightMergeableTowers(Tower selectedTower)
    {
        originalMaterials.Clear();
        //FAST REFACTOR TOMMAROW
        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            if (tower != selectedTower)
            {
                if (tower.TryGetComponent(out Tower currentTower))
                {
                    if (currentTower.faction == selectedTower.faction )
                    {
                        SpriteRenderer sr = tower.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            originalMaterials[tower] = sr.material;
                            sr.material = highlightMaterial;
                        }
                    }
                }
                
            }
        }
    }

    public void ClearMergeHighlights()
    {
        foreach (var pair in originalMaterials)
        {
            if (pair.Key != null)
            {
                SpriteRenderer sr = pair.Key.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.material = pair.Value; // Restore original material
                }
            }
        }
        originalMaterials.Clear();
    }
}
