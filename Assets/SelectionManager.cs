using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Tower  SelectedTower { get ; private set; }
    private Tower lastTowerSelect;
    public static SelectionManager Instance;
    public event System.Action<Plot> SelectionChanged;
    public Plot plot { get; private set; }
    [SerializeField] Tower tower;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {

        Plot.OnPlotClicked += ChangePlot;
    }
  
    public void ChangePlot(Plot chosen)
    {
        lastTowerSelect = SelectedTower;
        if (lastTowerSelect != null)
        {
        TowerManager.Instance.RemoveGlowEffect(lastTowerSelect.gameObject);

        }
       plot = chosen;
        if (plot.occupier!= null)
        {
         SelectedTower = plot.occupier;
         TowerManager.Instance.ApplyGlowEffect(SelectedTower.gameObject);
        }
    }
    // Update is called once per frame
    public void SetTower(TowerTemplate newTower)
    {
        SelectedTower = newTower.Tower;
        
    }
}
