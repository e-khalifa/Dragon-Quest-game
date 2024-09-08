using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifeTime;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;

        // If the projectile has been active for more than 5 seconds, deactivate it
        if (lifeTime > 5)
            Deactivate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Door"))
        {
            hit = true;
            boxCollider.enabled = false;
            animator.SetTrigger("explode");
            if (collision.CompareTag("Enemy"))
                collision.GetComponent<Health>().TakeDamage(1);
        }
    }
    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Flip the projectile's direction if necessary
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.lossyScale.y, transform.lossyScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
