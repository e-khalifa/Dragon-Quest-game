using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float range;
    [SerializeField] protected float damage;
    [SerializeField] protected LayerMask playerLayer;

    [Header("Collider Parameters")]
    [SerializeField] protected float colliderDistance;
    [SerializeField] protected BoxCollider2D boxCollider;

    protected float cooldownTimer = Mathf.Infinity;
    protected Animator anim;
    protected Health playerHealth;
    protected EnemyPatrol enemyPatrol;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }


    protected virtual bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
