using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject _retryButton;
    void Start()
    {
        _retryButton.SetActive(false);
        _videoPlayer.loopPointReached += OnVideoFinished;
    }
    private void Update()
    {
        if (_videoPlayer.isPrepared && _videoPlayer.length > 0
            && _videoPlayer.time >= _videoPlayer.length - 0.1)
        {
            OnVideoFinished(null);
        }
    }
    private void OnVideoFinished(VideoPlayer vp)
    {
        _videoPlayer.loopPointReached -= OnVideoFinished;
        _retryButton.SetActive(true);
    }
    public void OnRetryPressed()
    {
        SceneManager.LoadScene("InGame");
    }
}

