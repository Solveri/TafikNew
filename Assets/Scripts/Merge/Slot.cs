using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool IsEmpty { get; private set; } = true;
    [SerializeField] DraggableImage slotImage;

    private void Awake()
    {
       
    }

    public void SetEmpty(bool empty)
    {
        IsEmpty = empty;
    }
    public void AssignImage(DraggableImage image)
    {
        slotImage = image;
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableImage droppedImage = eventData.pointerDrag.GetComponent<DraggableImage>();

        if (droppedImage != null && droppedImage != slotImage)
        {
            Image draggedImage = droppedImage.GetComponent<Image>();

            if (IsEmpty)
            {
                // Place the new image in this slot
                droppedImage.parentAfterDrag = transform;
                droppedImage.transform.SetParent(transform);
                droppedImage.transform.localPosition = Vector3.zero;
                slotImage = droppedImage;
                droppedImage.currentSlot = this;
                SetEmpty(false);
            }
            else if (slotImage.image.sprite == draggedImage.sprite)
            {
                // If both images are identical, merge them
                MergeImages(droppedImage);
            }

        }
        LevelManager.instance.isHoldingImage = false;
    }

    private void MergeImages(DraggableImage draggedObject)
    {
        draggedObject.prevSlot.SetEmpty(true); // Mark the previous slot as empty
        Destroy(draggedObject.gameObject); // Remove the duplicate image

        // Example: Upgrade the tower image (replace with your logic)
        slotImage.image.sprite = GetNextLevelSprite(slotImage.image.sprite);
    }

    private Sprite GetNextLevelSprite(Sprite currentSprite)
    {
        // Add your merging/upgrade logic here (returning the same sprite for now)
        return currentSprite;
    }
}
