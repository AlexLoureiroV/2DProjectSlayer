using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private float _flashInterval = 0.1f;
    [SerializeField] private Color _flashColor = new Color(1f, 0.3f, 0.3f, 0.5f);

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Coroutine _flashCoroutine;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void StartFlash(float duration)
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashRoutine(duration));
    }

    private IEnumerator FlashRoutine(float duration)
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < duration)
        {
            _spriteRenderer.color = visible ? _flashColor : _originalColor;
            visible = !visible;
            yield return new WaitForSeconds(_flashInterval);
            elapsed += _flashInterval;
        }

        _spriteRenderer.color = _originalColor;
        _flashCoroutine = null;
    }
}