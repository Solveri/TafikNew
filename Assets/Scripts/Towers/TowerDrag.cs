using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    private Camera cam;
    private GameObject selectedTower;
    private Vector3 offset;
    private Vector3 originalPosition;

    public LayerMask plotLayer;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && selectedTower != null)
        {
            selectedTower.transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Tower"))
        {
            selectedTower = hit.collider.gameObject;
            originalPosition = selectedTower.transform.position;
            offset = selectedTower.transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseUp()
    {
        if (selectedTower != null)
        {
            Transform targetPlot = FindPlotAtMouseRelease();

            if (targetPlot != null)
            {
                Transform existingTower = GetExistingTower(targetPlot);

                if (existingTower != null)
                {
                    if (existingTower.gameObject.name == selectedTower.gameObject.name)
                    {
                        Debug.Log("Merge");
                        Destroy(selectedTower);
                    }
                    else
                    {
                        selectedTower.transform.position = originalPosition;
                    }
                }
                else
                {
                    selectedTower.transform.position = targetPlot.position;
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

    private Transform FindPlotAtMouseRelease()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, plotLayer);
        if (hit.collider != null && hit.collider.CompareTag("Plot"))
        {
            return hit.collider.transform;
        }
        return null;
    }

    private Transform GetExistingTower(Transform plot)
    {
        foreach (Transform child in plot)
        {
            if (child.CompareTag("Tower"))
            {
                return child;
            }
        }
        return null;
    }
}
