using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI Images

public class MergeManager : MonoBehaviour
{
    [SerializeField] private List<Slot> MergeSlots = new List<Slot>();
    [SerializeField] private DraggableImage towerOptions; // Sprite to show on occupied slot

    public void SpawnTower()
    {
        foreach (Slot slot in MergeSlots)
        {
            if (slot.IsEmpty)
            {
                // Create a new Image object inside the slot
                GameObject imageObject = new GameObject("SlotImage");
                imageObject.transform.SetParent(slot.transform, false); // Make it a child of the slot
                imageObject.transform.localPosition = Vector3.zero; // Center it in the slot

                // Add an Image component
                Image image = imageObject.AddComponent<Image>();
                image.sprite = towerOptions.image.sprite; // Assign the tower sprite

                // Set size to match slot (adjust as needed)
                RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = slot.GetComponent<RectTransform>().sizeDelta;

                // Mark the slot as occupied
                slot.SetEmpty(false);
                break;
            }
        }
    }
}
