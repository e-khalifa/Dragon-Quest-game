using UnityEngine;
using System.Collections;

public class GameIntroManager : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private CameraController cameraController;

    [Header("Princess settings")]
    [SerializeField] private Transform princess;
    [SerializeField] private float waitTimePrincess;

    [Header("Player settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float waitTimePlayer;


    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(waitTimePlayer);

        cameraController.MoveToTarget(princess);
        yield return new WaitForSeconds(waitTimePrincess);

        cameraController.MoveToTarget(player);
        cameraController.StartFollowingPlayer();
    }
}
