using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 1f;
    public bool isDark = false;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = isDark ? Color.red : Color.green;

        player = GameObject.Find("Player").transform;
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerActions>().isDark != isDark)
            {
                PlayerLifeManager.Instance.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
