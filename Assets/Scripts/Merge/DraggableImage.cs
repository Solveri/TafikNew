using System.Collections;
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
    public StarScript starScript;
  


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
              Tower newTower = plot.PlaceTower(Tower);
                
               newTower.currentPlot = plot;
                newTower.StarLevel = starScript.StarLevel;
                newTower.ApplyStarStats();
                prevSlot.SetEmpty(true);
                prevSlot = null;
                // Remove the draggable UI image
                LevelManager.instance.isHoldingImage = false;
                //make a function and move it to merge Script
                SpriteRenderer sr = newTower.GetComponent<SpriteRenderer>();
                switch (newTower.StarLevel)
                {
                    case 2:
                       sr.color = new Color(139, 0, 255);
                        break;
                    case 3:
                        sr.color = new Color(233, 255, 0);
                        break;
                    default:
                        sr.color = Color.white;
                        break;
                }

                Destroy(gameObject);
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
