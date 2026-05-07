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
    }

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
        float xPos = Random.Range(-7f, 7f);
        Vector2 itemPosition = new Vector2(xPos, transform.position.y);
        Item newItem = Instantiate(_currentZone.spawnList[index], itemPosition, Quaternion.identity);
        float torqueforce = Random.Range(-70f, 70f);
        newItem.GetComponent<Rigidbody2D>().AddTorque(torqueforce);

        // Dificultad progresiva por zona
        if (_currentMaxSpawnTime > _currentZone.minSpawnTime)
            _currentMaxSpawnTime -= 0.1f;
    }
}