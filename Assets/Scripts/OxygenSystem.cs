using UnityEngine;
using TMPro;

public class OxygenSystem : MonoBehaviour
{
    [SerializeField] private float _maxOxygen = 120f; // 2 minutos en segundos
    [SerializeField] private TextMeshProUGUI _oxygenText;

    private float _currentOxygen;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _currentOxygen = _maxOxygen;
    }

    private void Update()
    {
        _currentOxygen -= Time.deltaTime;
        _currentOxygen = Mathf.Clamp(_currentOxygen, 0, _maxOxygen);
        UpdateUI();

        if (_currentOxygen <= 0)
            _health.TakeDamage(); // O puedes llamar OnDeath directamente
    }

    public void AddOxygen(float percentage)
    {
        _currentOxygen += _maxOxygen * percentage;
    }

    private void UpdateUI()
    {
        float percentage = _currentOxygen / _maxOxygen * 100f;
        _oxygenText.text = Mathf.CeilToInt(percentage) + "%";
    }
}