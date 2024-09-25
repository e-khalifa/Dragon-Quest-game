using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform upperEdge;
    [SerializeField] private Transform lowerEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (PlayerIsWithinPatrolArea())
            FollowPlayer();
        else
            Patrol();
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            // Debug.Log("Moving Left: Enemy position = " + enemy.position.x + ", Left Edge = " + leftEdge.position.x);
            if (enemy.position.x > leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            // Debug.Log("Moving Right: Enemy position = " + enemy.position.x + ", Right Edge = " + rightEdge.position.x);
            if (enemy.position.x < rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                // Debug.Log("Reached Right Edge, changing direction.");
                DirectionChange();
            }
        }
    }
    private void FollowPlayer()
    {
        if (PlayerIsOnRight())
            MoveInDirection(1);
        else
            MoveInDirection(-1);
    }
    private bool PlayerIsWithinPatrolArea()
    {
        bool withinX = player.position.x > leftEdge.position.x && player.position.x < rightEdge.position.x;
        bool withinY = player.position.y > lowerEdge.position.y && player.position.y < upperEdge.position.y;
        return withinX && withinY;
    }
    private bool PlayerIsOnRight()
    {
        return player.position.x > enemy.position.x;
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft; // Toggle the direction
                                      // Debug.Log("Direction Changed: Now moving " + (movingLeft ? "Left" : "Right"));
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        // Debug.Log("Moving in Direction: " + _direction);

        // Flip the enemy sprite's direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        // Move the enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

}
