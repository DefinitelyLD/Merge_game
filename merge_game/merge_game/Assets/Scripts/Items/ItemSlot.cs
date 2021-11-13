using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isEmpty;
    public int id;
    public Vector2 slotPosition;
    public Item itemInSlot;

    private void Awake() 
    {
        slotPosition = GetComponent<RectTransform>().anchoredPosition;
        isEmpty = true;
    }

    public void OnDrop(PointerEventData eventData) 
    {
        if (eventData.pointerDrag != null) 
        {
            Item item = eventData.pointerDrag.GetComponent<Item>();
            if (isEmpty)
            {
                SlotManager.instance.ClearSlot(item.currentSlotID);
                item.ChangeSlot(slotPosition, id);
                itemInSlot = item;
                isEmpty = false;
            }
            else if (itemInSlot.itemType == item.itemType && isEmpty == false && itemInSlot.currentSlotID != item.currentSlotID && itemInSlot.itemType != (ItemType)(Enum.GetNames(typeof(ItemType)).Length - 1)) 
            {
                SlotManager.instance.ClearSlot(item.currentSlotID);
                itemInSlot.MergeItem();
                Destroy(item.gameObject);
            }
        }
    }
}
