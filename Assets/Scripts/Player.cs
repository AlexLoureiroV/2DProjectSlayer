using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    #region Fields
    [SerializeField] private Jetpack _jetpack;
    private Animator _anim;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("Player en: " + gameObject.name + " | RB: " + _rb + " | Jetpack: " + _jetpack);
    }

    void Update()
    {
        UpdateAnimator();
        UpdateFacing();
    }
    #endregion

    #region Private Methods
    private void UpdateAnimator()
    {
        _anim.SetBool("Flying", _jetpack.Flying);
        _anim.SetBool("Grounded", _jetpack.IsGrounded);
        _anim.SetFloat("SpeedX", Mathf.Abs(_rb.velocity.x));

        Debug.Log("Grounded: " + _jetpack.IsGrounded + " | SpeedX: " + Mathf.Abs(_rb.velocity.x));
    }

    private void UpdateFacing()
    {
        if (_rb.velocity.x > 0.1f)
            _spriteRenderer.flipX = true;
        else if (_rb.velocity.x < -0.1f)
            _spriteRenderer.flipX = false;
    }
    #endregion
}