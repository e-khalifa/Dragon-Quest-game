using UnityEngine;
using System.Collections;

public class GameIntroManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform princess; 
    [SerializeField] private CameraController cameraController; 
    [SerializeField] private float waitTimePrincess;
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
