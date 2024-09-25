using UnityEngine;

 //To Make Moving/Trackin Enemies/Traps only inside their rooms (Along With RoomEnemies class)

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player.x < Door.x ? he is on the left and going to the next room ? vice versa
            if (collision.transform.position.x < transform.position.x)
            {
                nextRoom.GetComponent<RoomEnemies>().ActivateEnemies(true);
                HandleRoomEnemies(previousRoom.GetComponent<RoomEnemies>());
            }
            else
            {
                previousRoom.GetComponent<RoomEnemies>().ActivateEnemies(true);
                HandleRoomEnemies(nextRoom.GetComponent<RoomEnemies>());
            }
        }
    }

    private void HandleRoomEnemies(RoomEnemies roomEnemies)
    {
        GameObject[] enemies = roomEnemies.enemies;

        foreach (var enemy in enemies)
        {
            if (enemy != null && IsCollidingWithBoundaries(enemy))
            {
                Destroy(enemy);
            }
        }
    }

    private bool IsCollidingWithBoundaries(GameObject enemy)
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(enemy.transform.position, 0.1f);

        foreach (var collider in collisions)
        {
            if (collider.CompareTag("Door") || collider.CompareTag("Wall") || collider.CompareTag("Ceiling"))
            {
                return true;
            }
        }

        return false;
    }*/
}
