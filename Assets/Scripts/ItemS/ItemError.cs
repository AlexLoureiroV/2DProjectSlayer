using UnityEngine;

public class ItemError : Item
{
    const float ERROR_FORCE = 10000;
    const float ERROR_DOWN_POS = 2.5f;

    #region Unity Callbacks
    protected override void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
            Recolected();
        if (collision.gameObject.CompareTag("Player"))
        {
            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
            if (jetpack.Flying)
                jetpack.GetComponent<Rigidbody2D>().AddForce(Vector2.down * ERROR_FORCE);
            else
                if (jetpack.transform.position.y > 1)
                jetpack.transform.Translate(Vector2.down * ERROR_DOWN_POS);
            Recolected();
        }
    }
    #endregion
}