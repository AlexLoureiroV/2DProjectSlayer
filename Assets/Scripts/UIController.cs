using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Fields
    [Header("Vidas")]
    [SerializeField] private Health _health;
    [SerializeField] private Image[] _lifeIcons;
    [SerializeField] private Sprite _lifeFullSprite;
    [SerializeField] private Sprite _lifeEmptySprite;
    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private TextMeshProUGUI _textHeight;
    [SerializeField] private Image[] _energyCells;
    [SerializeField] private Color[] _cellFullColors; // define aquí el color de cada celda
    [SerializeField] private Color _cellEmptyColor = new Color(0.15f, 0.15f, 0.15f);
    [SerializeField] private Color _cellLowColor = new Color(1f, 0.3f, 0.1f);

    private float _maxEnergy;
    private bool _initialized;
    #endregion

    #region Unity Callbacks
    void Update()
    {
        if (!_initialized)
        {
            if (_jetpack.Energy > 0)
            {
                _maxEnergy = _jetpack.MaxEnergy;
                _initialized = true;
            }
            return;
        }

        _textHeight.text = ((int)_jetpack.transform.position.y).ToString();
        UpdateEnergyCells();
    }
    #endregion

    #region Private Methods
    void Start()
    {
        // Suscribirse al evento de cambio de vidas
        if (_health != null)
            _health.OnLivesChanged.AddListener(UpdateLivesUI);

        UpdateLivesUI(_health != null ? _health.Lives : 3);
    }

    private void UpdateLivesUI(int currentLives)
    {
        for (int i = 0; i < _lifeIcons.Length; i++)
        {
            if (_lifeIcons[i] != null)
                _lifeIcons[i].sprite = i < currentLives ? _lifeFullSprite : _lifeEmptySprite;
        }
    }



    private void UpdateEnergyCells()

    {
        if (_maxEnergy == 0 || !_initialized) return;

        float energyPercent = _jetpack.Energy / _maxEnergy;
        int cellsFilled = Mathf.CeilToInt(energyPercent * _energyCells.Length);

        for (int i = 0; i < _energyCells.Length; i++)
        {
            if (i < cellsFilled)
            {
                Color fullColor = (i < _cellFullColors.Length) ? _cellFullColors[i] : Color.white;
                _energyCells[i].color = energyPercent <= 0.2f ? _cellLowColor : fullColor;
            }
            else
                _energyCells[i].color = _cellEmptyColor;
        }
    }
    #endregion
}