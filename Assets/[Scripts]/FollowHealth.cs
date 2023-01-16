using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TargetFollow
{
    public Transform transform;
    public Vector3 offset;
    public bool x;
    public bool y;
    public bool z;
}


public class FollowHealth : MonoBehaviour
{
    public TargetFollow target;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            (target.x) ? target.transform.position.x + target.offset.x : transform.position.x,
            (target.y) ? target.transform.position.y + target.offset.y : transform.position.y,
            (target.z) ? target.transform.position.z + target.offset.z : transform.position.z
            );       
    }
}
