using TMPro;
using UnityEngine;

public class BossHeadFire : EnemyBullet
{
    [SerializeField] private Animator DamageAnimator;
    [SerializeField] private TextMeshPro damageText;
    [SerializeField] private EnemyHitStop hitStop;
    private float bulletLife = 300f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet") || collision.CompareTag("Orbital"))
        {
            int damage = ikarugaColor.isDark != collision.gameObject.GetComponent<IkarugaColor>().isDark
                ? collision.CompareTag("Bullet") ? player.bulletDamage : player.orbitalDamage
                : 0;

            bulletLife -= player.bulletDamage;
            damageText.text = damage.ToString();
            DamageAnimator.SetTrigger("Hit");
            hitStop.TriggerHitStop();

            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }

            if (bulletLife <= 0)
            {
                SFXManager.instance.PlaySound("BossAttackExplosion");
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
