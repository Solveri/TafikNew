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
    public Tower Tower;

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
        LevelManager.instance.isHoldingImage = true;
        transform.position = eventData.position; // Move image with cursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if the image was dropped onto a Plot in the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Plot plot = hit.collider.GetComponent<Plot>();

            if (plot != null && !plot.isOccupied)
            {
                // Place a tower on the detected Plot
                plot.PlaceTower(Tower);
                Destroy(gameObject);
                Tower.currentPlot = plot;
                prevSlot.SetEmpty(true);
                prevSlot = null;
                // Remove the draggable UI image
                LevelManager.instance.isHoldingImage = false;
                return;
            }
        }

        // If dropped back in a UI slot, update slot information
        if (parentBeforeDrag != parentAfterDrag)
        {
            if (prevSlot != null)
            {
                prevSlot.SetEmpty(true);
                prevSlot.AssignImage(null);
            }
        }
        else
        {
            currentSlot = prevSlot;
        }

        transform.SetParent(parentAfterDrag); // Return to original or new parent
        transform.position = parentAfterDrag.position;
        LevelManager.instance.isHoldingImage = false;
        canvasGroup.blocksRaycasts = true; // Enable raycasts again
    }
}
