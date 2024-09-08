using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player.x < Door.x ? he is on the left and going to the next room ? vice versa
            if (collision.transform.position.x < transform.position.x)
            {
                cameraController.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<RoomEnemies>().ActivateEnemies(true);
                previousRoom.GetComponent<RoomEnemies>().ActivateEnemies(false);
            }
            else
            {
                cameraController.MoveToNewRoom(previousRoom);
                previousRoom.GetComponent<RoomEnemies>().ActivateEnemies(true);
                nextRoom.GetComponent<RoomEnemies>().ActivateEnemies(false);

            }
        }
    }
}