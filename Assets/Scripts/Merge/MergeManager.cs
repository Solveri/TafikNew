using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [SerializeField] List<Slot> MergeSlots = new List<Slot>();
    [SerializeField] Sprite towerOptions;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnTower()
    {
        foreach (Slot slot in MergeSlots)
        {
            if (slot.IsEmpty)
            {
                //ADD RANDOM TOWER
                break;
            }
        }
    }
}
