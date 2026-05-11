using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Jetpack : MonoBehaviour
{
    public enum Direction { Left, Right }

    #region Properties

    public float Energy
    {
        get { return _energy; }
        set
        {
            _energy = Mathf.Clamp(value, 0, _maxEnergy);
            CheckLowEnergy();
        }
    }
    public float MaxEnergy => _maxEnergy;
    public bool Flying { get; set; }
    public bool IsGrounded { get; private set; }

    #endregion

    #region Fields

    // Components
    private Rigidbody2D _targetRB;
    private AudioSource _audioSource;

    // Energy
    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energyFlyingRatio;
    [SerializeField] private float _energyRegenerationRatio;

    // Movement
    [SerializeField] private float _horizontalForce;
    [SerializeField] private float _flyForce;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _airFriction = 0.98f;
    [SerializeField] private float _jumpForce = 10f;


    // Ground Check
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    // Audio
    public AudioClip lowEnergySound;

    #endregion

    #region Unity Callbacks


    private void Awake()
    {
        _targetRB = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        lowEnergySound = Resources.Load<AudioClip>("Loop_urgencia");
    }
    public void Jump()
    {
        bool canJump = IsGrounded || IsTouchingWallBelow();
        Debug.Log("Jump llamado | IsGrounded: " + IsGrounded + " | WallBelow: " + IsTouchingWallBelow());

        if (canJump)
            _targetRB.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool IsTouchingWallBelow()
    {
        // Lanza un raycast hacia abajo para detectar Wall y solo saltar en ese caso
        RaycastHit2D hit = Physics2D.CircleCast(
            transform.position,
            _groundCheckRadius,
            Vector2.down,
            _groundCheckRadius,
            _groundLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
            return true;

        return false;
    }

    // Energía baje del 20% Y se pulse el botón
    public void StartLowEnergySound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = lowEnergySound;
            _audioSource.loop = true; 
            _audioSource.Play();
        }
    }

    // Parar sonido de emergencia cuando suelte el botón O la energía suba del 20%
    public void StopLowEnergySound()
    {
        
        if (_audioSource.isPlaying)
        {
            StartCoroutine(StopAfterCurrentLoop());
        }
    }

    private IEnumerator StopAfterCurrentLoop()
    {
        float clipLength = lowEnergySound.length;
        float timeRemaining = clipLength - (_audioSource.time % clipLength);

        // Desactiva el loop para que no repita
        _audioSource.loop = false;

        // Espera a que termine la reproducción actual
        yield return new WaitForSeconds(timeRemaining);

        _audioSource.Stop();
    }

    private void Start()
    {
        Energy = _maxEnergy;
    }


    private void FixedUpdate()
    {
        
        IsGrounded = Physics2D.OverlapCircle(
            _groundCheck.position,
            _groundCheckRadius,
            _groundLayer
        );

        if (Flying)
            DoFly();

        if (IsGrounded && !Flying)
            Regenerate();
    }

    #endregion

    #region Public Methods

    public void FlyUp() { Flying = true; }
    public void StopFlying() { Flying = false; }
    public void Regenerate() { Energy += _energyRegenerationRatio; }
    public void AddEnergy(float energy) { Energy += energy; }

    public void FlyHorizontal(Direction flyDirection)
    {
        if (!Flying) return;
        _targetRB.AddForce(flyDirection == Direction.Left ? Vector2.left * _horizontalForce : Vector2.right * _horizontalForce);
    }

    public void WalkHorizontal(Direction walkDirection)
    {
        if (Flying || !IsGrounded) return;
        float dir = (walkDirection == Direction.Left) ? -1f : 1f;
        _targetRB.velocity = new Vector2(dir * _walkSpeed, _targetRB.velocity.y);
    }

    public void StopWalking()
    {
        if (!IsGrounded)
        {
            _targetRB.velocity = new Vector2(_targetRB.velocity.x * _airFriction, _targetRB.velocity.y);
            return;
        }
        _targetRB.velocity = new Vector2(0f, _targetRB.velocity.y);
    }

    #endregion

    #region Private Methods

    private void DoFly()
    {
        if (Energy > 0)
        {
            _targetRB.AddForce(Vector2.up * _flyForce);
            Energy -= _energyFlyingRatio;
        }
        else
            Flying = false;
    }

    private void CheckLowEnergy()
    {
        if (_audioSource == null || lowEnergySound == null)
        {
           
            return;
        }

        float percentage = _energy / _maxEnergy;
        Debug.Log("Energía: " + percentage);

        if (percentage <= 0.2f)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = lowEnergySound;
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    #endregion
}