using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Effect", menuName = "Items/New Effect")]
public class Effect : ScriptableObject
{
    public int p_str, p_int, p_dex, p_def, p_sta = 0;
    public bool isTrash = false;

    public void UseEffect()
    {
        if (isTrash)
        {
            var status = GameObject.FindObjectOfType<CharacterStatus>();
            status.ResetStatus();
        }
        else
        {
            var status = GameObject.FindObjectOfType<CharacterStatus>();
            status.GetPotion(p_str, p_dex, p_int, p_def, p_sta);
        }
    }
}
