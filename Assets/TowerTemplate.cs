using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerTemplate : MonoBehaviour
{
    public Tower Tower;
    [SerializeField] Button button;
    [SerializeField] Sprite Image;
    // need to change it to scriptable object
    // Start is called before the first frame update
    void Start()
    {
        button.image.sprite = Image; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
