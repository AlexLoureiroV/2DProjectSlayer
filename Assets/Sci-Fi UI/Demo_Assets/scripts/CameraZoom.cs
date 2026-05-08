using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Jetpack _jetpack;

    [Header("Zoom")]
    [SerializeField] private float _normalFOV = 60f;
    [SerializeField] private float _zoomFOV = 40f;
    [SerializeField] private float _zoomThreshold = 0.2f;
    [SerializeField] private float _zoomSpeed = 5f;

    private float _targetFOV;

    void Start()
    {
        _targetFOV = _normalFOV;
        if (_virtualCamera != null)
            _virtualCamera.m_Lens.FieldOfView = _normalFOV;
    }

    void Update()
    {
        if (_jetpack == null || _virtualCamera == null) return;

        float energyPercent = _jetpack.Energy / _jetpack.MaxEnergy;
        _targetFOV = energyPercent <= _zoomThreshold ? _zoomFOV : _normalFOV;

        _virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards(
            _virtualCamera.m_Lens.FieldOfView,
            _targetFOV,
            Time.deltaTime * _zoomSpeed
        );
    }
}