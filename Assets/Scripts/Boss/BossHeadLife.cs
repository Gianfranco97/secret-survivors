using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHeadLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private Animator animatorHead;
    [SerializeField] private Animator animatorDamageText;
    [SerializeField] private TextMeshPro damageText;
    [SerializeField] private Slider bossTotalHealt;
    [SerializeField] private EnemyHitStop hitStop;
    [SerializeField] private GameObject ikarugaShield;
    private LifeManager life;
    private PlayerActions player;
    private float deathTime = 1f;
    private IkarugaColor ikarugaColor;
    private Coroutine shieldCorutine;

    private void Start()
    {
        life = new LifeManager(maxHealth);
        player = GameObject.Find("Player").GetComponent<PlayerActions>();
        ikarugaColor = GetComponent<IkarugaColor>();
        ikarugaShield.GetComponent<IkarugaColor>().isDark = ikarugaColor.isDark;
    }

    private IEnumerator ShowIkarugaShield()
    {
        ikarugaShield.SetActive(true);
        ikarugaShield.GetComponent<Animator>().SetBool("IsDark", ikarugaColor.isDark);
        yield return new WaitForSecondsRealtime(0.2f);
        ikarugaShield.SetActive(false);
    }

    private IEnumerator TakeDamage(int damage, bool isInvulnerable)
    {
        if (isInvulnerable)
        {
            if (shieldCorutine != null) StopCoroutine(shieldCorutine);
            shieldCorutine = StartCoroutine(ShowIkarugaShield());
            yield break;
        }

        SFXManager.instance.PlaySound("BossHit");
        damageText.text = damage.ToString();
        animatorDamageText.SetTrigger("Hit");
        hitStop.TriggerHitStop();
        bossTotalHealt.value = bossTotalHealt.value - Mathf.Min(damage, life.currentHealth);
        life.TakeDamage(damage);

        if (life.currentHealth <= 0)
        {
            SFXManager.instance.PlaySound("BossHeadDead");
            animatorHead.SetTrigger("Dead");
            yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Orbital"))
        {
            IkarugaColor bullet = collision.gameObject.GetComponent<IkarugaColor>();
            bool isInvulnerable = ikarugaColor.isDark == bullet.isDark;
            int damage = isInvulnerable
                    ? 0 : collision.CompareTag("Bullet") ? player.bulletDamage : player.orbitalDamage;

            StartCoroutine(TakeDamage(damage, isInvulnerable));
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
