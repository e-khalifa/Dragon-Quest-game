using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            playerHealth.CurrentHealth = 0;
            playerHealth.Died();
            return;
        }
        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckpoint = collision.transform;
            SoundManager.Instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}