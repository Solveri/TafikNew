using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentAfterDrag;
    public Transform parentBeforeDrag;
    public Image image;
    private CanvasGroup canvasGroup;
    public Slot currentSlot;
    public Slot prevSlot;

    private void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevSlot = currentSlot;
        currentSlot = null;
        parentBeforeDrag = transform.parent;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Move to top layer while dragging
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false; // Allow raycast to pass through while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Move image with mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentBeforeDrag != parentAfterDrag)
        {
            prevSlot.SetEmpty(true);
            prevSlot.AssignImage(null);
        }
        else
        {
            currentSlot = prevSlot;
        }
        transform.SetParent(parentAfterDrag); // Return to original or new parent
        transform.position = parentAfterDrag.position;
        canvasGroup.blocksRaycasts = true; // Enable raycasts again
    }
}
