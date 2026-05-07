using UnityEngine;

public class ItemAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupSound;

    public void PlayPickupSound()
    {
        AudioManager.Instance?.PlaySFX(_pickupSound);
    }
}