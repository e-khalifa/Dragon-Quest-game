using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    private Animator animator;
    private bool dead;
    private float damageTaken;
    public float CurrentHealth;
    private UIManager uiManager;


    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOffFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("SFX")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private PlayerRespawn playerRespawn;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
        uiManager = FindAnyObjectByType<UIManager>(); //first active loaded obj of that type
    }
    public void TakeDamage(float _damage)
    {
        //min and max value
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);

            animator.SetTrigger("hurt");
            damageTaken++;
            Debug.Log("Damage Taken: " + damageTaken);
            StartCoroutine(Invulnerability());
            // Coroutines are used to handle operations that take time without blocking the main, like async in dart
            // Works with IEnumerator
            // Yield statement is like await

            // Reset "hurt" trigger after a short delay to allow transition
            Invoke(nameof(ResetHurtTrigger), 0.1f);
            if (damageTaken == 5)
            {
                StartCoroutine(HandleRespawn());
            }
        }

        else
         if (!dead)

            Died();
    }

    public void AddHealth(float _value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + _value, 0, startingHealth);
        damageTaken = 0;
    }

    private IEnumerator Invulnerability()
    {
        Debug.Log("Invulnerability started.");

        // Disable collision between layers 8 and 9
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOffFlashes; i++)
        {
            // Change color to indicate invulnerability
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));

            // Revert to original color
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
        }

        // Re-enable collision between layers 8 and 9 after invulnerability period ends
        Physics2D.IgnoreLayerCollision(8, 9, false);

        Debug.Log("Invulnerability ended.");
    }

    private IEnumerator HandleRespawn()
    {
        damageTaken = 0; // Reset damage counter
        SoundManager.instance.PlaySound(deathSound);
        animator.SetTrigger("die");
        dead = true;

        // Wait for the die animation to finish
        yield return new WaitForSeconds(1.0f);
        playerRespawn.CheckRespawn();
    }

    public void Respawn()
    {
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invulnerability());

        foreach (Behaviour component in components)
            component.enabled = true;
        dead = false;
    }

    public void Died()
    {
        animator.SetTrigger("die");
        foreach (Behaviour component in components)
            component.enabled = false;
        dead = true;
        SoundManager.instance.PlaySound(deathSound);
        if (gameObject.CompareTag("Player"))
            uiManager.GameOver();
    }
    private void ResetHurtTrigger()
    {
        animator.ResetTrigger("hurt");
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
