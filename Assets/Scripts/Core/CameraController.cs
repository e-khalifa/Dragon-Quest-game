using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform player;

    public void StartFollowingPlayer()
    {
        virtualCamera.Follow = player;
    }

    public void MoveToTarget(Transform target)
    {
        virtualCamera.Follow = target;
    }
}
