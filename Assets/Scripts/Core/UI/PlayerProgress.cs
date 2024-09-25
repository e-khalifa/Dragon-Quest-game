using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private float targetProgress;

    private PlayerMovement playerMovement;

    void Start()
    {
        progressBar.value = 0f;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdateTargetProgress();
        progressBar.value = Mathf.Lerp(progressBar.value, targetProgress, playerMovement.moveSpeed * Time.deltaTime);
    }

    void UpdateTargetProgress()
    {
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        float currentDistance = Vector3.Distance(player.position, startPoint.position);
        targetProgress = Mathf.Clamp01(currentDistance / totalDistance);
    }
}
