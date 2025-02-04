using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public Material highlightMaterial; // Assign in Inspector
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void HighlightMergeableTowers(GameObject selectedTower)
    {
        originalMaterials.Clear();
        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            if (tower != selectedTower && tower.name == selectedTower.name)
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
