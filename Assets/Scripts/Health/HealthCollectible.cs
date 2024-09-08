using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header("SFX")]
    [SerializeField] private AudioClip pickUpHeart;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Health>().AddHealth(healthValue);
            SoundManager.instance.PlaySound(pickUpHeart);
            gameObject.SetActive(false);
        }
    }
}
