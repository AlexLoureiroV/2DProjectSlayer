using UnityEngine;

public class HazardProjectile : MonoBehaviour
{
    [SerializeField] private string _destroyOnTag = "Ground";
    [SerializeField] private float _destroyDelay = 0f; // 0 = instantáneo

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(_destroyOnTag))
        {
            if (_destroyDelay <= 0f)
                Destroy(gameObject);
            else
                Destroy(gameObject, _destroyDelay);
        }
    }
}