using UnityEngine;
using TMPro;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goalText;
    [SerializeField] private string _message = "¡Nivel completado!";
    [SerializeField] private float _waitTime = 3f; // Segundos antes de cargar la siguiente escena
    [SerializeField] private string _nextScene = "NombreDeLaSiguienteEscena";

    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered) return;
        if (!other.CompareTag("Player")) return;

        _triggered = true;
        _goalText.text = _message;
        _goalText.gameObject.SetActive(true);

        Time.timeScale = 0f; // Pausa el juego
        StartCoroutine(LoadNextScene());
    }

    private System.Collections.IEnumerator LoadNextScene()
    {
        // WaitForSecondsRealtime ignora timeScale
        yield return new WaitForSecondsRealtime(_waitTime);
        Time.timeScale = 1f; // Restaura el tiempo
        UnityEngine.SceneManagement.SceneManager.LoadScene(_nextScene);
    }
}