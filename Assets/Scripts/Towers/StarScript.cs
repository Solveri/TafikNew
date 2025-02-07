using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public const int MAX_STAR_LEVEL = 3;
    public int StarLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetStarLevel(int level)
    {
        StarLevel = level;
    }
    public bool TryMergeTowers(StarScript otherTower)
    {
        if (StarLevel != MAX_STAR_LEVEL && otherTower.StarLevel == StarLevel)
        {
            StarLevel++;
            Debug.Log(StarLevel);
            return true;
            
        }
        else
        {
            return false;
        }
    }
}

