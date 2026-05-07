using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<Health>()?.TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<Health>()?.TakeDamage();
    }
}