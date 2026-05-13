using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private TextMeshProUGUI _pressStartText;
    [SerializeField] private float _skipAfterSeconds = 2f; // puedes saltar a partir de este momento

    private bool _videoFinished = false;

    void Start()
    {
       
    }

    void Update()
    {
        // Detectar fin de vídeo por tiempo (más fiable que el evento)
        if (!_videoFinished && _videoPlayer.isPrepared)
        {
            double duration = _videoPlayer.length;
            if (duration > 0 && _videoPlayer.time >= duration - 0.1)
            {
                _videoFinished = true;
            }
        }

        // Permitir saltar a partir de _skipAfterSeconds
        bool canSkip = _videoPlayer.time >= _skipAfterSeconds;

        if (canSkip && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("InGame");
        }

        // Mostrar texto "Press Start" cuando el vídeo ha terminado
        if (_videoFinished && !_pressStartText.gameObject.activeSelf)
        {
            _pressStartText.gameObject.SetActive(true);
        }
    }
}