using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [Header("Collectable Settings")]
    [SerializeField] private float healthValue;

    [Header("SFX")]
    [SerializeField] private AudioClip pickUpHeart;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Health>().AddHealth(healthValue);
            SoundManager.Instance.PlaySound(pickUpHeart);
            gameObject.SetActive(false);
        }
    }
}
