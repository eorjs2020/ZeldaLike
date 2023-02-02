using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    GameObject inventoryPanel;

    private int maxInventorySlot = 11;
    public int currentInventorySlot = 0;
    void Start()
    {
        //Read all itemSlots as children of inventory panel
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
        maxInventorySlot = itemSlots.Count;
        for(int i = 0; i < maxInventorySlot; i++)
        {
            if(itemSlots[i].item != null)
            {
                ++currentInventorySlot;
            }
        }        
    }

    public int FindSameItem(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {            
            ItemSlot slot = itemSlots[i];
            if (slot.item == item)
                return i;           
        }
        return -1;
    }

    public void GetItem(Item item)
    {
        if (FindSameItem(item) != -1)
        {
           itemSlots[FindSameItem(item)].Count++;

        }
        else
        {
            if (maxInventorySlot > currentInventorySlot)
            {
                int i = 0;
                for (i = 0; i < maxInventorySlot; i++)
                {
                    if (itemSlots[i].item == null)
                        break;
                }
                itemSlots[i].item = item;
                itemSlots[i].Count++;
                currentInventorySlot++;
            }
        }
        
    }

    public int CheckItemSlot()
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot.item == null)
                return i;
        }
        return -1;
    }
     
}
