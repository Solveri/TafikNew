using Unity.Burst.CompilerServices;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    private Camera cam;
    private Tower selectedTower;
    private Vector3 offset;
    private Vector3 originalPosition;
    private GameObject towerObject;

    public LayerMask plotLayer;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (GameManager.Instance.isGamePasued)
        {
            return;
        }
        if (Input.GetMouseButton(0) && selectedTower != null)
        {
            selectedTower.transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.transform.TryGetComponent(out selectedTower))
        {
            originalPosition = selectedTower.gameObject.transform.position;
            offset = selectedTower.transform.position - GetMouseWorldPos();

            // Highlight mergeable towers
            TowerManager.Instance.HighlightMergeableTowers(selectedTower);
        }
    }

    private void OnMouseUp()
    {
        if (selectedTower != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            TowerManager.Instance.ClearMergeHighlights(); // Remove highlights

            // should use the plot itself to find the tower 
            Plot targetPlot = FindPlotAtMouseRelease();

            if (targetPlot != null)
            {
                Tower existingTower = GetExistingTower(targetPlot);

                if (existingTower != null)
                {
                    Tower existingTowerScript = existingTower.GetComponent<Tower>();
                    if (selectedTower.gameObject.CompareTag(existingTowerScript.gameObject.tag) && selectedTower != existingTowerScript && selectedTower.StarLevel == existingTower.StarLevel)
                    {
                        Debug.Log("Merge");
                        existingTowerScript.StarLevel++;
                        existingTowerScript.ApplyStarStats();
                        selectedTower.currentPlot.isOccupied = false;
                        selectedTower.currentPlot.occupier = null;
                        SpriteRenderer sr = existingTower.GetComponent<SpriteRenderer>();
                        switch (existingTower.StarLevel)
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
                        Destroy(selectedTower.gameObject);

                    }
                    else
                    {
                        selectedTower.transform.position = originalPosition;
                    }
                }
                else
                {
                    selectedTower.transform.position = originalPosition;
                }
            }
            else
            {
                selectedTower.transform.position = originalPosition;
            }
            selectedTower = null;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    private Plot FindPlotAtMouseRelease()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, plotLayer);
        if (hit.collider != null && hit.collider.CompareTag("Plot"))
        {
            return hit.collider.gameObject.GetComponent<Plot>();
        }
        return null;
    }

    private Tower GetExistingTower(Plot plot)
    {
        return plot.occupier;
    }
}
