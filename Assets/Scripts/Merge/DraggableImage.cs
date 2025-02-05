using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentAfterDrag;
    public Image image;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        transform.SetParent(parentAfterDrag); // Return to original or new parent
        canvasGroup.blocksRaycasts = true; // Enable raycasts again
    }
}
