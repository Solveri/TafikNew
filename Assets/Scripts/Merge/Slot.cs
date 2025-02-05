using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool IsEmpty { get; private set; } = true;
    private Image slotImage;

    private void Awake()
    {
        slotImage = GetComponentInChildren<Image>(); // Find an image inside the slot
    }

    public void SetEmpty(bool empty)
    {
        IsEmpty = empty;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableImage droppedImage = eventData.pointerDrag.GetComponent<DraggableImage>();

        if (droppedImage != null)
        {
            Image draggedImage = droppedImage.GetComponent<Image>();

            if (IsEmpty)
            {
                // Place the new image in this slot
                droppedImage.transform.SetParent(transform);
                droppedImage.transform.localPosition = Vector3.zero;
                slotImage = draggedImage;
                SetEmpty(false);
            }
            else if (slotImage.sprite == draggedImage.sprite)
            {
                // If both images are identical, merge them
                MergeImages(droppedImage.gameObject);
            }
        }
    }

    private void MergeImages(GameObject draggedObject)
    {
        Destroy(draggedObject); // Remove the duplicate image

        // Example: Upgrade the tower image (replace with your logic)
        slotImage.sprite = GetNextLevelSprite(slotImage.sprite);
    }

    private Sprite GetNextLevelSprite(Sprite currentSprite)
    {
        // Add your merging/upgrade logic here (returning the same sprite for now)
        return currentSprite;
    }
}
