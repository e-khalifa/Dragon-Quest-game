using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private AudioClip swordSound;

    protected override void Update()
    {
        base.Update();

        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("meleeAttack");
            SoundManager.Instance.PlaySound(swordSound);
        }
        else
        {
            anim.SetBool("moving", enemyPatrol != null && enemyPatrol.enabled);
        }
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
