using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;

    private float knockBack;

    private List<Collider> alreadyCollideWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollideWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == myCollider) { return; }

        if(alreadyCollideWith.Contains(other)) { return; }

        alreadyCollideWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 force = (other.transform.position - myCollider.transform.position).normalized * knockBack;
            forceReceiver.AddForce(force);
        }

    }

    public void SetAttack(int damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }
}
