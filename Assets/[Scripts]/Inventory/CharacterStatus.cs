using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI STR;
    [SerializeField]
    private TMPro.TextMeshProUGUI DEX;
    [SerializeField]
    private TMPro.TextMeshProUGUI INT;
    [SerializeField]
    private TMPro.TextMeshProUGUI DEF;
    [SerializeField]
    private TMPro.TextMeshProUGUI STA;

    private int Str, Int, Dex, Def, Sta = 0;
    

    public void GetPotion(int m_str, int m_dex, int m_int, int m_def, int m_sta)
    {
        Str += m_str;
        Dex += m_dex;
        Int += m_int;
        Def += m_def;
        Sta += m_sta;

        STR.text = Str.ToString();
        DEX.text = Dex.ToString();
        INT.text = Int.ToString();
        DEF.text = Def.ToString();
        STA.text = Sta.ToString();
    }

    public void ResetStatus()
    {
        Str = 0;
        Dex = 0;
        Int = 0;
        Def = 0;
        Sta = 0;

        STR.text = Str.ToString();
        DEX.text = Dex.ToString();
        INT.text = Int.ToString();
        DEF.text = Def.ToString();
        STA.text = Sta.ToString();
    }
}
