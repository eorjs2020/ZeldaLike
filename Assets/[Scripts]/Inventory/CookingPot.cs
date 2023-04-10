using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingPot : MonoBehaviour
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    public Recipe result;
    public Recipe trash;
    public Recipe[] recipes;
    public int maxItem = 4;
    public int itemNum = 0;
    int checkSlot;
    public Image resultImage;
    // Start is called before the first frame update
    public void Start()
    {
        itemSlots = new List<ItemSlot>(
            gameObject.transform.GetComponentsInChildren<ItemSlot>()
            );
        recipes = Resources.LoadAll<Recipe>("Recipes");
    }

    public void GetItem(Item item)
    {
        int i;
        for(i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].item == null)
                break;
        }        
        itemSlots[i].item = item;
        itemSlots[i].Count++;
        ++itemNum;        
    }

    public void RemoveItem()
    {
        --itemNum;        
    }

    private void Update()
    {
        
    }

    public void Cooking()
    {
        if (CheckIsIngrediant())
        {
            int recipeNum = CheckRecipes();

            if (recipeNum == -1)
                result = trash;
            else
                result = recipes[recipeNum];
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if(itemSlots[i].item != null)
                    itemSlots[i].Count--;
                
            }
            resultImage.sprite = result.result.icon;
            itemNum = 0;
        }
    }

    public bool CheckIsIngrediant()
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].item != null)
                return true;
        }
        Debug.Log("NoItem");
        return false;
    }

    public int CheckRecipes()
    {
        for (int i = 0; i < recipes.Length; i++)
        {            
            int check = 0;
            for (int j = 0; j < itemSlots.Count; j++)
            {                 
                if (itemSlots[j].item == recipes[i].ingredients[j])
                {
                    ++check;
                    if (check >= 4)
                    {
                        return i;
                    }
                }
            }

        }
        return -1;
    }

    public void GiveItem()
    {
        if(result != null)
        {
            var iven = GameObject.FindObjectOfType<Inventory>();
            iven.GetItem(result.result);
            resultImage.sprite = null;
            result = null;
        }
    }

}
