using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private float _invulnerabilityDuration = 2f;
    [SerializeField] private float _fallDeathY = -10f; // Y por debajo de la cual muere

    public int Lives { get; private set; }
    public bool IsInvulnerable { get; private set; }

    public UnityEvent<int> OnLivesChanged;  // para actualizar la UI
    public UnityEvent OnDeath;              // cuando se acaban las vidas
    public UnityEvent OnDamaged;            // para efectos visuales/sonido

    private Vector3 _respawnPoint;
    private float _invulnerabilityTimer;
    private DamageFlash _damageFlash;
    private PlayerAudio _playerAudio;

    void Awake()
    {
        Lives = _maxLives;
        _respawnPoint = transform.position;
        _damageFlash = GetComponent<DamageFlash>();
        _playerAudio = GetComponent<PlayerAudio>();
    }

    void Update()
    {
        // Bajar invulnerabilidad
        if (IsInvulnerable)
        {
            _invulnerabilityTimer -= Time.deltaTime;
            if (_invulnerabilityTimer <= 0f)
                IsInvulnerable = false;
        }

        // Muerte por caída
        if (transform.position.y < _fallDeathY)
            TakeDamage();
    }

    public void SetRespawnPoint(Vector3 point)
    {
        _respawnPoint = point;
    }

    public void TakeDamage()
    {
        if (IsInvulnerable) return;

        Lives--;
        OnLivesChanged?.Invoke(Lives);
        OnDamaged?.Invoke();
        _playerAudio?.PlayDamageSound();

        if (Lives <= 0)
        {
            Lives = 0;
            OnDeath?.Invoke();
        }
        else
        {
            // Solo invulnerabilidad temporal, sin mover al jugador
            IsInvulnerable = true;
            _invulnerabilityTimer = _invulnerabilityDuration;
            _damageFlash?.StartFlash(_invulnerabilityDuration);
        }
    }

    public void HealLife()
    {
        if (Lives >= _maxLives) return;
        Lives++;
        OnLivesChanged?.Invoke(Lives);
    }
    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

}