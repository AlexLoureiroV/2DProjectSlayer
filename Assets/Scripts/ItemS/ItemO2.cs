using UnityEngine;

public class ItemO2 : Item
{
    [SerializeField] private float _oxygenAmount = 0.05f;

    protected override void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Invoke("DestroySelf", 2f);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            OxygenSystem oxygen = collision.gameObject.GetComponent<OxygenSystem>();
            if (oxygen != null)
                oxygen.AddOxygen(_oxygenAmount);
            Recolected();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}