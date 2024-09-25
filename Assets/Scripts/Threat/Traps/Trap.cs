using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected float damage;
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            collider.GetComponent<Health>().TakeDamage(damage);
    }
}
