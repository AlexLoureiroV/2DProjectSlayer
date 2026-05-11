using UnityEngine;
public class O2BottleDestroy : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 2f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")) return;
        Invoke("DestroySelf", _destroyDelay);
    }

    private void DestroySelf()
    {
        Debug.Log("DESTRUYENDO bombona");
        foreach (Collider2D col in GetComponents<Collider2D>())
            col.enabled = false;
        Destroy(gameObject);
    }
}