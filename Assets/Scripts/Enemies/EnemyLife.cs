using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private GameObject energyPrefab;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;
    [SerializeField] private GameObject ikarugaShield;
    private EnemiesCounter enemiesCounter;
    private LifeManager life;
    private EnemyHitStop hitStop;
    private PlayerActions player;
    private float deathTime = 0.3f;
    private IkarugaEnemy ikarugaEnemy;
    private GameObject itemsContainer;
    private Coroutine shieldCorutine;

    private void Start()
    {
        hitStop = GetComponent<EnemyHitStop>();
        life = new LifeManager(maxHealth);
        enemiesCounter = GameObject.Find("GameManager").GetComponent<EnemiesCounter>();
        player = GameObject.Find("Player").GetComponent<PlayerActions>();
        ikarugaEnemy = GetComponent<IkarugaEnemy>();
        itemsContainer = GameObject.Find("ItemsContainer");
        ikarugaShield.GetComponent<IkarugaColor>().isDark = ikarugaEnemy.isDark;
    }

    private void GenerateEnergy()
    {
        var item = Instantiate(energyPrefab, itemsContainer.transform);
        item.transform.localPosition = transform.position;
    }

    private void IncreaseCounter()
    {
        enemiesCounter.AddDefeatEnemy(enemyType, GetComponent<IkarugaEnemy>().isDark);
    }

    private IEnumerator ShowIkarugaShield()
    {
        ikarugaShield.SetActive(true);
        ikarugaShield.GetComponent<Animator>().SetBool("IsDark", ikarugaEnemy.isDark);
        yield return new WaitForSecondsRealtime(0.2f);
        ikarugaShield.SetActive(false);
    }

    private IEnumerator TakeDamage(int damage, bool isInvulnerable)
    {
        hitStop.TriggerHitStop();

        if (isInvulnerable)
        {
            if (shieldCorutine != null) StopCoroutine(shieldCorutine);
            shieldCorutine = StartCoroutine(ShowIkarugaShield());
            yield break;
        }

        if (damage <= 0) yield break;

        damageText.text = damage.ToString();
        life.TakeDamage(damage);
        animator.SetTrigger("Hit");

        yield return new WaitForSecondsRealtime(deathTime);

        if (life.currentHealth <= 0)
        {
            IncreaseCounter();
            bool itemGenerated = player.GenerateLuckItem(transform.position);
            if (!itemGenerated) GenerateEnergy();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Orbital"))
        {
            IkarugaColor bullet = collision.gameObject.GetComponent<IkarugaColor>();
            bool isInvulnerable = ikarugaEnemy.isDark == bullet.isDark;
            int damage = isInvulnerable ? 0 : collision.CompareTag("Bullet") ? player.bulletDamage : player.orbitalDamage;

            StartCoroutine(TakeDamage(damage, isInvulnerable));
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
