using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image _fadePanel;
    [SerializeField] private float _fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        float elapsed = 0f;
        Color color = _fadePanel.color;
        color.a = 1f;
        _fadePanel.color = color;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - (elapsed / _fadeDuration);
            _fadePanel.color = color;
            yield return null;
        }

        color.a = 0f;
        _fadePanel.color = color;
        _fadePanel.gameObject.SetActive(false);
    }
}