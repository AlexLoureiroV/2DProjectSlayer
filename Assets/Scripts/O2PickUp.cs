using UnityEngine;
public class O2PickUp : MonoBehaviour
{
    public AudioClip pickUpSound;
    public float volume = 1f;
    public GameObject pickUpEffect;
    [Range(0f, 1f)] public float oxygenAmount = 0.05f;
    private bool _collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_collected) return;
        Debug.Log("TRIGGER con: " + other.name + " | Tag: " + other.tag, other.gameObject);
        if (!other.CompareTag("Player")) return;

        _collected = true;

        OxygenSystem oxygen = other.GetComponent<OxygenSystem>();
        if (oxygen != null)
            oxygen.AddOxygen(oxygenAmount);

        if (pickUpEffect != null)
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(pickUpSound, transform.position, volume);
        Destroy(gameObject);
    }
}