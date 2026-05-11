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
        if (_particles == null) return; // Evita el crash cuando no hay un item asignado a la zona 
        Instantiate(_particles, transform.position, Quaternion.identity);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision); 
    }

    protected virtual void OnCollision(Collision2D collision)
    {
        
    }
    #endregion
}
