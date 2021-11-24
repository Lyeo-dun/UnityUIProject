using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryCtrl : MonoBehaviour
{
    [SerializeField] private List<GameObject> Slots;

    private void Awake()
    {
        GameObject SlotParents = GameObject.Find("InventoryWindow/SetBar/Scroll View/Viewport/Content");
        
        for(int i = 0; i < SlotParents.transform.childCount; i++)
        {
            Slots.Add(SlotParents.transform.GetChild(i).gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
}
