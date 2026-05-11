using UnityEngine;
using System;

public class Item : MonoBehaviour, IRecolectable
{
	#region Enums
	public enum ItemTypes
	{
		None,
		NoSe,
		ErrorCode,
		PositiveWords
	}
	#endregion

	#region Properties
	[field: SerializeField] public ItemTypes Type { get; set; }
	#endregion

	#region Fields
	[SerializeField] private GameObject _particles;
	#endregion

	#region Public Methods
	public virtual void Recolected()
	{
		Destroy(gameObject);
		CreateParticles();
	}
    #endregion

    #region Private Methods
    private void CreateParticles()
    {
        if (_particles == null) return; // Evita el crash
        Instantiate(_particles, transform.position, Quaternion.identity);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
    #endregion
}
