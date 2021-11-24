using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryCtrl : MonoBehaviour
{
    [SerializeField] private List<GameObject> Slots;
    [SerializeField] private GameObject Contain;

    private void Awake()
    {
        GameObject SlotParents = transform.GetChild(0).GetChild(1).Find("Slots").gameObject;
        
        for(int i = 0; i < SlotParents.transform.childCount; i++)
        {
            Slots.Add(SlotParents.transform.GetChild(i).gameObject);
        }

        Contain = transform.GetChild(0).GetChild(1).Find("Contain").gameObject;
    }

    public bool SlotCollsionItem(Vector2 _Pos, Vector2 _Scale, out GameObject _slot)
    {
        Vector2 SlotPos = Vector2.zero;
        Vector2 SlotScale = Vector2.zero;

        foreach(var Slot in Slots)
        {
            SlotPos = Slot.GetComponent<RectTransform>().anchoredPosition;
            SlotScale = Slot.GetComponent<RectTransform>().sizeDelta;

            //아이템과 슬롯이 부딪히는 중이라면
            if(((SlotPos.x + SlotScale.x / 2) > (_Pos.x - _Scale.x / 2))
                && (SlotPos.y + SlotScale.y / 2)  > (_Pos.y - _Scale.y / 2)
                && ((SlotPos.x - SlotScale.x / 2) < (_Pos.x + _Scale.x / 2))
                && (SlotPos.y - SlotScale.y / 2) < (_Pos.y + _Scale.y / 2))
            {
                _slot = Slot;
                return true;
            }
        }

        _slot = null;
        return false;
    }

    public bool isSlotEmpty(GameObject Slot)
    {
        Transform data = Slot.transform.GetChild(0);

        if(data.childCount > 0)
            return false;

        return true;
    }

    public int EmptySlotIndex()
    {
        int _index = 0;

        foreach(var Slot in Slots)
        {
            if(isSlotEmpty(Slot))
            {
                break;
            }
            _index++;
        }

        if (_index == Slots.Count)
            _index = -1;

        return _index;
    }

    public void ItemInSlot(int _SlotIndex, GameObject Item)
    {
        Transform data = Slots[_SlotIndex].transform.GetChild(0);

        Item.transform.SetParent(data);

        Vector2 pos = data.GetComponent<RectTransform>().anchoredPosition;
        ItemSettingPos(pos, data.gameObject, Item);
    }

    public void ItemInSlot(GameObject _SlotObejct, GameObject Item)
    {
        Item.transform.SetParent(_SlotObejct.transform);

        Vector2 pos = _SlotObejct.GetComponent<RectTransform>().anchoredPosition;
        ItemSettingPos(pos, _SlotObejct, Item);
    }

    public void ItemOutSlot(GameObject Item)
    {
        Item.transform.SetParent(Contain.transform);
    }

    public void ItemSettingPos(Vector2 pos, GameObject parent,GameObject Item)
    {
        pos.x += (parent.GetComponent<RectTransform>().sizeDelta.x - Item.GetComponent<RectTransform>().sizeDelta.x) / 2;
        pos.y += -1 * (parent.GetComponent<RectTransform>().sizeDelta.y - Item.GetComponent<RectTransform>().sizeDelta.y) / 2;
        Item.GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
