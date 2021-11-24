using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCtrl : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private IventoryCtrl InventorySystem;
    
    [SerializeField] private GameObject CurrentParent;


    private void Awake()
    {
        InventorySystem = transform.parent.gameObject.GetComponent<IventoryCtrl>();
    }
    void Start()
    {
        if(InventorySystem.EmptySlotIndex() == -1)
        {
            Debug.LogError("아이템을 넣을수 없는 상태!");
            Destroy(gameObject);
            return;
        }

        InventorySystem.ItemInSlot(InventorySystem.EmptySlotIndex(), gameObject);
        CurrentParent = transform.parent.gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySystem.ItemOutSlot(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform ItemRect = GetComponent<RectTransform>();

        GameObject InSlot = null;

        if (!InventorySystem.SlotCollsionItem(ItemRect.anchoredPosition, ItemRect.sizeDelta, out InSlot))
        {
            InventorySystem.ItemInSlot(CurrentParent, gameObject);
            return;
        }

        CurrentParent = InSlot.transform.GetChild(0).gameObject;
        InventorySystem.ItemInSlot(CurrentParent, gameObject);

    }
}
