using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotManager : MonoBehaviour
{
    public static SlotManager instance;

    [SerializeField] GameObject itemPrefab;

    [SerializeField] List<ItemSlot> slots;
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < slots.Count; i++)
            slots[i].id = i;
    }
    public void InitializeSlotManager(bool[] states, int[] items)
    {
        for (int i = 0; i < states.Length; i++)
        slots[i].isEmpty = states[i];

        int counter = 0;

        foreach (ItemSlot slot in slots)
        if (slot.isEmpty == false)
        {   
            GameObject obj = Instantiate(itemPrefab, slot.slotPosition, Quaternion.identity, canvas.transform);
            Item item = obj.GetComponent<Item>();
            item.InitializeItem(canvas, slot.slotPosition, slot.id, (ItemType)items[counter]);

            slot.itemInSlot = item;
            counter++;
        }
    }
    public void ClearSlot(int id) => slots[id].isEmpty = true;

    public void TurnRaycasts(bool isActive) 
    {
        foreach (ItemSlot slot in slots)
        if (slot.isEmpty == false) 
        slot.itemInSlot.TurnRayCast(isActive);
    }

    public void GenerateItem() 
    {
        foreach (ItemSlot slot in slots) 
        if (slot.isEmpty) 
        {
            GameObject obj = Instantiate(itemPrefab, slot.slotPosition, Quaternion.identity, canvas.transform);
            Item item = obj.GetComponent<Item>();
            item.InitializeItem(canvas, slot.slotPosition, slot.id, ItemType.level_1);

            slot.isEmpty = false;
            slot.itemInSlot = item;
            break;
        }
    }

    public bool FindItemInSlots(ItemType itemType)
    {
        foreach (ItemSlot slot in slots)
        if (slot.isEmpty == false && slot.itemInSlot.itemType == itemType) 
        {
            slot.itemInSlot.transform.DOLocalMove(new Vector3(0f, 450f, 0f) , 1f, false).OnComplete(() => DeleteItemFromSlot(slot));
            return true;
        }
        return false;
    }

    private void DeleteItemFromSlot(ItemSlot slot) 
    {
        Destroy(slot.itemInSlot.gameObject);
        ClearSlot(slot.id);
    }
    #region Saving Getters
    public bool[] GetSlotsEmptyState() 
    {
        bool[] slotsStates = new bool[8];

        for (int i = 0; i < slots.Count; i++)
        slotsStates[i] = slots[i].isEmpty;

        return slotsStates;
    }
    public int[] GetSlotsItems() 
    {
        int[] slotsItems = new int[8];

        int counter = 0;
        foreach (ItemSlot slot in slots)
        if(slot.isEmpty == false) 
        {
            slotsItems[counter] = (int)slot.itemInSlot.itemType;
            counter++;
        }

        return slotsItems;
    }
    #endregion
}
