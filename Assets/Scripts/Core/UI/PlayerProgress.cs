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

        // Smoothly interpolate the progress bar value using Mathf.Lerp
        // Use playerMovement.moveSpeed here, assuming it controls the player's speed
        progressBar.value = Mathf.Lerp(progressBar.value, targetProgress, playerMovement.moveSpeed * Time.deltaTime);
    }

    void UpdateTargetProgress()
    {
        // Calculate the progress as a percentage of the journey completed
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        float currentDistance = Vector3.Distance(player.position, startPoint.position);

        // Clamp the progress between 0 and 1 to avoid overshooting
        targetProgress = Mathf.Clamp01(currentDistance / totalDistance);
    }
}
