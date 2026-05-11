using UnityEngine;

public class WallBounce : MonoBehaviour
{
    [SerializeField] private float _bounceMultiplier = 1.5f;
    [SerializeField] private float _minBounceForce = 3f;
    [SerializeField] private float _maxBounceForce = 8f; 

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall")) return;

        Vector2 bounceDirection = collision.contacts[0].normal;
        float impactForce = Mathf.Max(collision.relativeVelocity.magnitude, _minBounceForce);

        // Para limitar la fuerza máxima de rebote
        float bounceForce = Mathf.Min(impactForce * _bounceMultiplier, _maxBounceForce);

        _rb.velocity = bounceDirection * bounceForce;
    }
}