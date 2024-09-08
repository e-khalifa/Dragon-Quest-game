using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator animator;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        // If the left mouse button is clicked, cooldown time has passed, and the player can attack
        if (Input.GetMouseButton(0) && coolDownTimer > attackCoolDown && playerMovement.CanAttack())
            Attack(); // Perform attack

        coolDownTimer += Time.deltaTime; // Increment cooldown timer by the time passed since last frame
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        animator.SetTrigger("attack");

        coolDownTimer = 0;

        // Object pooling => fireballs

        fireballs[FindFireball()].transform.position = firePoint.position; // Set fireball's position to firePoint
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));  // Set fireball's direction based on player orientation
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            // If a fireball is not active in the scene, return its index
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0; // Default to the first fireball if all are active
    }


}
