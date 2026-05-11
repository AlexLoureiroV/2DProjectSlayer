using UnityEngine;

public class ItemCometa : Item
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _angle = 45f;

    private Vector2 _direction;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Elige aleatoriamente izquierda o derecha
        bool goingLeft = Random.value > 0.5f;

        float xDir = goingLeft ? -1f : 1f;
        _direction = new Vector2(xDir * Mathf.Cos(_angle * Mathf.Deg2Rad),
                                 -Mathf.Sin(_angle * Mathf.Deg2Rad)).normalized;

        // Espeja el sprite según la dirección
        _spriteRenderer.flipX = !goingLeft;
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    protected override void OnCollisionEnter2D(Collision2D collision) { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Recolected();
    }
}