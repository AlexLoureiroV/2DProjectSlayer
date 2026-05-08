using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _footstepSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _jetpackSound;

    private AudioSource _jetpackAudioSource;
    private Jetpack _jetpack;

    void Awake()
    {
        _jetpack = GetComponent<Jetpack>();
        // AudioSource dedicado para el jetpack (loop)
        _jetpackAudioSource = gameObject.AddComponent<AudioSource>();
        _jetpackAudioSource.clip = _jetpackSound;
        _jetpackAudioSource.loop = true;
        _jetpackAudioSource.playOnAwake = false;
    }

    void Update()
    {
        // Sonido jetpack en loop mientras vuela
        if (_jetpack.Flying && !_jetpackAudioSource.isPlaying)
            _jetpackAudioSource.Play();
        else if (!_jetpack.Flying && _jetpackAudioSource.isPlaying)
            _jetpackAudioSource.Stop();
    }

    // Llamado desde Animation Event
    public void PlayFootstep()
    {
        Debug.Log("PlayFootstep | Grounded: " + _jetpack.IsGrounded + " | Flying: " + _jetpack.Flying + " | clip: " + _footstepSound);
        if (_jetpack.IsGrounded && !_jetpack.Flying)
            AudioManager.Instance?.PlaySFX(_footstepSound);
    }

    // Llamado desde Health.OnDamaged
    public void PlayDamageSound()
    {
        AudioManager.Instance?.PlaySFX(_damageSound);
    }
}