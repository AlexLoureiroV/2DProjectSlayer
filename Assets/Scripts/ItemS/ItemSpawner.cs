using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnZone
    {
        public string zoneName;
        public List<Item> spawnList;
        public float minSpawnTime = 1f;
        public float maxSpawnTime = 5f;
        public Collider2D zoneBounds;
    }
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<SpawnZone> _zones;

    private SpawnZone _currentZone;
    private float _nextSpawnTime;
    private float _cronoTime = 0;
    private float _currentMaxSpawnTime;

    void Start()
    {
        if (_zones.Count > 0)
            SetZone(0);
    }

    void Update()
    {
        if (_currentZone == null || _currentZone.spawnList.Count == 0) return;

        Debug.Log("Zona: " + _currentZone.zoneName + " | Crono: " + _cronoTime + " | NextSpawn: " + _nextSpawnTime + " | MaxSpawn: " + _currentMaxSpawnTime + " | Min: " + _currentZone.minSpawnTime);

        _cronoTime += Time.deltaTime;
        if (_cronoTime > _nextSpawnTime)
        {
            SpawnItem();
            ResetTime();
        }
    }

    public void SetZone(int zoneIndex)
    {
        if (zoneIndex < 0 || zoneIndex >= _zones.Count) return;

        // Destruir todos los items activos al cambiar de zona
        GameObject[] items = GameObject.FindGameObjectsWithTag("HazardItem");
        foreach (GameObject item in items)
            Destroy(item);

        _currentZone = _zones[zoneIndex];
        _currentMaxSpawnTime = _currentZone.maxSpawnTime;
        ResetTime();
        Debug.Log("Zona activa: " + _currentZone.zoneName);
    }

    private void ResetTime()
    {
        _cronoTime = 0;
        _nextSpawnTime = Random.Range(_currentZone.minSpawnTime, _currentMaxSpawnTime);
    }

    private void SpawnItem()
    {
        int index = Random.Range(0, _currentZone.spawnList.Count);

        Vector2 itemPosition;

        if (_currentZone.zoneBounds != null)
        {
            Bounds bounds = _currentZone.zoneBounds.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            // Solo por encima del jugador, hasta el techo de la zona
            float minY = _playerTransform != null ? _playerTransform.position.y : bounds.min.y;
            float y = Random.Range(minY, bounds.max.y);
            itemPosition = new Vector2(x, y);
        }
        else
        {
            float xPos = _playerTransform != null
                ? _playerTransform.position.x + Random.Range(-7f, 7f)
                : Random.Range(-7f, 7f);
            float yPos = _playerTransform != null
                ? _playerTransform.position.y + Random.Range(0f, 10f)
                : transform.position.y;
            itemPosition = new Vector2(xPos, yPos);
        }

        Item newItem = Instantiate(_currentZone.spawnList[index], itemPosition, Quaternion.identity);
        float torqueforce = Random.Range(-70f, 70f);
        newItem.GetComponent<Rigidbody2D>().AddTorque(torqueforce);

        if (_currentMaxSpawnTime > _currentZone.minSpawnTime)
            _currentMaxSpawnTime -= 0.1f;
    }
}