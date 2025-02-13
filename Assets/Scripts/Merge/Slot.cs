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
            else if (slotImage.starScript.TryMergeTowers(droppedImage.starScript))
            {
                droppedImage.prevSlot.SetEmpty(true); // Mark the previous slot as empty
                Destroy(droppedImage.gameObject); // Destroy the dragged image
            }

        }
        LevelManager.instance.isHoldingImage = false;

        switch (slotImage.starScript.StarLevel)
        {
            case 2:
                slotImage.image.color = new Color(139, 0, 255);
                break;
            case 3:
                slotImage.image.color = new Color(233, 255, 0);
                break;
            default:
                slotImage.image.color = Color.white;
                break;
        }
    }
}
