using UnityEngine;
using TMPro;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] private float _totalTime = 60f;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Health _health;

    private float _timeRemaining;

    void Start()
    {
        _timeRemaining = _totalTime;
        UpdateText();
    }

    void Update()
    {
        if (_timeRemaining <= 0f) return;

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            UpdateText();
            _health.RestartScene();
        }
        else
        {
            UpdateText();
        }
    }

    private void UpdateText()
    {
        float percent = (_timeRemaining / _totalTime) * 100f;
        _timerText.text = "OXIGEN " + Mathf.CeilToInt(percent) + "%";
    }
}