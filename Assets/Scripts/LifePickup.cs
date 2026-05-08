using UnityEngine;

public class LifePickUp : MonoBehaviour
{
    public AudioClip pickUpSound;
    public float volume = 1f;
    public GameObject pickUpEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tu lógica de vida aquí...

            if (pickUpEffect != null)
                Instantiate(pickUpEffect, transform.position, Quaternion.identity);

            AudioSource.PlayClipAtPoint(pickUpSound, transform.position, volume);
            Destroy(gameObject);
        }
    }
}