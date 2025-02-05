using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI Images

public class MergeManager : MonoBehaviour
{
    [SerializeField] private List<Slot> MergeSlots = new List<Slot>();
    [SerializeField] private DraggableImage towerPrefab; // Prefab of the Draggable Tower

    public void SpawnTower()
    {
        foreach (Slot slot in MergeSlots)
        {
            if (slot.IsEmpty)
            {
                // Instantiate the draggable tower
                DraggableImage newTower = Instantiate(towerPrefab, slot.transform);
                newTower.transform.localPosition = Vector3.zero; // Center it in the slot
                slot.AssignImage(newTower);
                newTower.currentSlot = slot;
                // Mark the slot as occupied
                slot.SetEmpty(false);
                break;
            }
        }
    }
}

