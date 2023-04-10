using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public void Interaction()
    {
        ChangeGameManager.Instance.IventoryOpen();        
        Debug.Log("AA");
    }
}
