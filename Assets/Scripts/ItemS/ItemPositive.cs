using UnityEngine;
using System;

public class ItemPositive : Item
{
    #region Constants
    const float POSITIVE_HEAL = 20;
    #endregion

    #region Unity Callbacks
    protected override void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
            Recolected();
        if (collision.gameObject.CompareTag("Player"))
        {
            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
            jetpack.AddEnergy(POSITIVE_HEAL);
            Recolected();
        }
    }
    #endregion
}