using UnityEngine;
public class LifePickUp : MonoBehaviour
{
    public AudioClip pickUpSound;
    public float volume = 1f;
    public GameObject pickUpEffect;
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
                health.HealLife(healAmount);

            if (pickUpEffect != null)
                Instantiate(pickUpEffect, transform.position, Quaternion.identity);

            AudioSource.PlayClipAtPoint(pickUpSound, transform.position, volume);
            Destroy(gameObject);
        }
    }
}