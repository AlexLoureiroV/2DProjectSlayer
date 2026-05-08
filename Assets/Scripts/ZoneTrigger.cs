using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private int _zoneIndex;

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Player"))
            _itemSpawner.SetZone(_zoneIndex);
    }

}