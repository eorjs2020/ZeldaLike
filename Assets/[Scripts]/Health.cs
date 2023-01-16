using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;

    public int health;
    public bool isPlayer = false;
    private bool isInvunerable = false;

    public event Action OnTakeDamage;

    public event Action OnDie;
    public HealthBarController controller;
    public bool IsDead => health == 0;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvunerable(bool isInvunerable)
    {
        this.isInvunerable = isInvunerable;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; }

        if (isInvunerable) { return; }
        health = Mathf.Max(health - damage, 0);
        if(!isPlayer)
            controller?.TakeDamage(damage);
        else
            ChangeGameManager.Instance.TakeDamage(damage);
        OnTakeDamage?.Invoke();

        if(health == 0)
        {
            if(!isPlayer)
                controller.gameObject.SetActive(false);
            OnDie?.Invoke();
        }

        Debug.Log(health);
    }
}
