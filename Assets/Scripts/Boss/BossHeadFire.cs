using UnityEngine;

public class BossHeadFire : EnemyBullet
{
    [SerializeField] private EnemyHitStop hitStop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet") || collision.CompareTag("Orbital"))
        {
            hitStop.TriggerHitStop();

            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
