using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
public class Recipe : ScriptableObject
{
    // Start is called before the first frame update
    public List<Item> ingredients;
    public Item result;
}
