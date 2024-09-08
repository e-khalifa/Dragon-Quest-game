using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Room Camera")]
    [SerializeField] private float cameraSpeed;
    private float targetPosX;
    private Vector3 velocity = Vector3.zero;

    // //FollowPlayer
    // [Header("Follow Player")]
    // [SerializeField] private Transform player;
    // [SerializeField] private float aheadDistance;
    // private float lookAhead;

    private void Update()
    {
        //Room camera - smoothDamp: from 
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosX, transform.position.y, transform.position.z), ref velocity, cameraSpeed);

        // //Follow Player
        // transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        // lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed); //Only to make it smooth and gradual
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        targetPosX = _newRoom.position.x;
    }
}
