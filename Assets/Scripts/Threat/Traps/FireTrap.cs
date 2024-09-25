using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header("SFX")]
    [SerializeField] private AudioClip fireSound;
    private bool triggered;
    private bool active;

    private Health playerHealth;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHealth = collider.GetComponent<Health>();

            // Start the firetrap activation coroutine if not already triggered
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && active)
        {
            // Damage the player if the trap is active
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            playerHealth = null;
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        SoundManager.Instance.PlaySound(fireSound);

        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);

        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}
