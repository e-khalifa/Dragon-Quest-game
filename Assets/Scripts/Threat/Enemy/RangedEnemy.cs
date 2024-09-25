using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    protected override void Update()
    {
        base.Update();

        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("rangedAttack");
            RangedAttack();
        }
        else
        {
            anim.SetBool("moving", enemyPatrol != null && enemyPatrol.enabled);
        }
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
