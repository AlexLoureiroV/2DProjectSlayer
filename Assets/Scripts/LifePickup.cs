using UnityEngine;

public class LifePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.GetComponent<Health>();
        if (health != null)
        {
            health.HealLife();
            GetComponent<ItemAudio>()?.PlayPickupSound();
            Destroy(gameObject);
        }
    }
}