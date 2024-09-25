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
        uiManager = FindAnyObjectByType<UIManager>();
    }
    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            SoundManager.Instance.PlaySound(hurtSound);
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
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOffFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private IEnumerator HandleRespawn()
    {
        damageTaken = 0;
        SoundManager.Instance.PlaySound(deathSound);
        animator.SetTrigger("die");
        dead = true;

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
        SoundManager.Instance.PlaySound(deathSound);
        if (gameObject.CompareTag("Player"))
        {

            uiManager.GameOver();
            Deactivate();

        }
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
