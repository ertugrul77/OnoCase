using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private List<Transform> cameraMoveTargetList;
    [SerializeField] private float movementTime;

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineComposer _composer;
    private const int TargetIndex = 0;
    private const float TargetScreenX = 0.7f;
    private const float Duration = 1f;
    private Vector3 _startPos;
    private float _startScreenX;
    
    private void Awake()
    {
        EventManager.Instance.OnHousePressed += EventManager_OnHousePressed;
        EventManager.Instance.OnBackButtonPressed += EventManager_BackButtonPressed;
        
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineComposer>();

        _startPos = transform.position;
        _startScreenX = _composer.m_ScreenX;
    }

    private void EventManager_BackButtonPressed()
    {
        transform.DOMove(_startPos, movementTime).OnComplete(() =>
        {
            SetCameraOffsets(_startScreenX, true);
        });
    }

    private void EventManager_OnHousePressed()
    {
        transform.DOMove(cameraMoveTargetList[TargetIndex].position, movementTime).OnComplete(() =>
        {
            SetCameraOffsets(TargetScreenX, false);
        });
    }

    private void SetCameraOffsets(float toTarget, bool mainPos)
    {
        DOVirtual.Float(_composer.m_ScreenX, toTarget, Duration, value =>
        {
            _composer.m_ScreenX = value;
        });
    }
}
