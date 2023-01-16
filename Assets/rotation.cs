using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    float x;
    public Transform rotationTransform;
    public bool isShield = false;
    // Update is called once per frame
    void Update()
    {
        x += 50 * Time.deltaTime;
        rotationTransform.eulerAngles = new Vector3(0, x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            var manager = GameObject.FindObjectOfType<ChangeGameManager>();
            if(!isShield)
                manager.isWeapon = true;
            else
                manager.isShield = true;
            Destroy(this.gameObject);
        }
    }
}
