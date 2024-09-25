using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;
    private float coolDownTimer;

    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= attackCoolDown)
            Attack();
    }

    private void Attack()
    {
        SoundManager.Instance.PlaySound(arrowSound);
        coolDownTimer = 0;
        arrows[Findarrow()].transform.position = firePoint.position;
        arrows[Findarrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int Findarrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }


}
