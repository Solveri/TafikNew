using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform[] PathPoints;
    public Transform startPoint;
    public Transform endPoint;
    public List<Plot> plotsRenders = new List<Plot>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
       
    }
    // Start is called before the first frame update
    void Start()
    {
        ShowSlots(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowSlots(bool changeStates)
    {
        foreach (var plot in plotsRenders)
        {
            if (plot.isOccupied)
            {
                continue;
            }
            plot.spriteRenderer.enabled = changeStates;
            
        }
    }
}
