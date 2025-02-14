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

    /// <summary>
    /// Highlights all mergeable towers of the same faction.
    /// </summary>
    public void HighlightMergeableTowers(Tower selectedTower)
    {
        originalMaterials.Clear();

        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            if (tower != selectedTower && tower.TryGetComponent(out Tower currentTower))
            {
                if (currentTower.faction == selectedTower.faction)
                {
                    ApplyGlowEffect(tower);
                }
            }
        }
    }

    /// <summary>
    /// Applies a glow effect to a specific tower.
    /// </summary>
    public void ApplyGlowEffect(GameObject tower)
    {
        if (tower.TryGetComponent(out SpriteRenderer sr))
        {
            if (!originalMaterials.ContainsKey(tower))
            {
                originalMaterials[tower] = sr.material; // Store original material
            }
            sr.material = highlightMaterial; // Apply glow effect
        }
    }

    /// <summary>
    /// Removes the glow effect from a specific tower.
    /// </summary>
    public void RemoveGlowEffect(GameObject tower)
    {
        if (tower.TryGetComponent(out SpriteRenderer sr) && originalMaterials.ContainsKey(tower))
        {
            sr.material = originalMaterials[tower]; // Restore original material
            originalMaterials.Remove(tower);
        }
    }

    /// <summary>
    /// Clears all highlighted towers.
    /// </summary>
    public void ClearMergeHighlights()
    {
        foreach (var pair in originalMaterials)
        {
            if (pair.Key != null && pair.Key.TryGetComponent(out SpriteRenderer sr))
            {
                sr.material = pair.Value; // Restore original material
            }
        }
        originalMaterials.Clear();
    }
}
