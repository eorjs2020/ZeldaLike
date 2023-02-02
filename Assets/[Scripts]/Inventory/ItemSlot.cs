using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item = null;
    public bool isCookingPot = false;
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [SerializeField]
    private int count = 0;
    private CookingPot cookingPot;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            UpdateGraphic();
        }
    }    

    [SerializeField]
    Image itemIcon;

    [SerializeField]
    TextMeshProUGUI itemCountText;

    // Start is called before the first frame update
    void Start()
    {
        cookingPot = GameObject.FindObjectOfType<CookingPot>();
        UpdateGraphic();
    }

    //Change Icon and count
    public void UpdateGraphic()
    {
        if (count < 1)
        {
            item = null;
            itemIcon.gameObject.SetActive(false);
            itemCountText.gameObject.SetActive(false);
        }
        else
        {
            if(!isCookingPot)
            {
                Inventory inven = GameObject.FindObjectOfType<Inventory>();
                inven.currentInventorySlot--;
            }
            //set sprite to the one from the item
            itemIcon.sprite = item.icon;
            itemIcon.gameObject.SetActive(true);
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = count.ToString();
        }
    }

    public void UseItemInSlot()
    {
        if (CanUseItem())
        {            
            if (item.isConsumable)
            {
                item.Use();
                Count--;                
            }
            else
            {
                if (cookingPot.itemNum < cookingPot.maxItem)
                {                    
                    cookingPot.GetItem(item);
                    --Count;
                }
            }
           
        }
    }

    public void UseItemInCookingPot()
    {
        Inventory inven = GameObject.FindObjectOfType<Inventory>();
        inven.GetItem(item);
        Count--;
        CookingPot cook = GameObject.FindObjectOfType<CookingPot>();
        cook.RemoveItem();

    }
    private bool CanUseItem()
    {        
        return item != null && count > 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = item.description;
            nameText.text = item.name;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }
}
