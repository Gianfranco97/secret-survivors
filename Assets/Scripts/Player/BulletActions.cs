using UnityEngine;

public class BulletActions : MonoBehaviour
{
    public int damage = 1;
    public bool isDark = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = isDark ? Color.red : Color.green;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IkarugaEnemy enemyIkaruga = collision.GetComponent<IkarugaEnemy>();
            if (enemyIkaruga.isDark != isDark)
            {
                collision.GetComponent<EnemyLife>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
