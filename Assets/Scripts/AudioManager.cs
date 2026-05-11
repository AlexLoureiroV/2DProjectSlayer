using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Música")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _backgroundMusic;

    [Header("Sonidos")]
    [SerializeField] private AudioSource _sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Crea los AudioSource automáticamente si no están asignados
        if (_musicSource == null)
            _musicSource = gameObject.AddComponent<AudioSource>();

        if (_sfxSource == null)
            _sfxSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        if (_backgroundMusic != null)
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        Debug.Log("PlaySFX | clip: " + clip + " | sfxSource: " + _sfxSource);
        if (clip != null)
            _sfxSource.PlayOneShot(clip, volume);
    }
}