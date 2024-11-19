using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LuckItem
{
    public GameObject item;
    public int chance = 10;
}

public class PlayerActions : MonoBehaviour
{
    public float knockBackStrength = 16, knockBackDelay = 0.15f, knockBackDistance = 10;
    public int level = 0, energyTotal = 0, energyRequiredToTheNextLevel = 20;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI levelText;
    public float speed = 100f;
    [SerializeField] private float enemyDamageOnTouch = 2f;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private GameObject CardContainer;
    [SerializeField] private CardsGenerator cardsGenerator;
    [SerializeField] private ObitalWeapon obitalWeapon;
    [SerializeField] private AudioSource ikarugaAudioSource;
    [SerializeField] private VariantsSFX energySFX;
    public Animator circlePlayerAnimator;
    public IkarugaColor ikarugaColor;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 moveSpeed;
    public float luck = 3;
    public List<LuckItem> luckItems;
    private List<GameObject> luckItemsFormated = new List<GameObject>();
    public int bulletDamage = 10;
    public int orbitalDamage = 5;
    private GameObject itemsContainer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        itemsContainer = GameObject.Find("ItemsContainer");
        UpdateEnergySlider();
        FormatLuckItems();
    }

    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ikarugaAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            ikarugaAudioSource.Play();
            ikarugaColor.SwitchColor();
            obitalWeapon.ChangeObitalsColor();
        }
    }

    void MovePlayer()
    {
        moveSpeed = moveDirection * speed;
        rb.velocity = new Vector2(moveSpeed.x, moveSpeed.y);
    }

    private void UpdateEnergySlider()
    {
        energySlider.maxValue = energyRequiredToTheNextLevel;
        energySlider.value = energyTotal;
        levelText.text = $"Level: {level}";
    }

    public void PlayEnemiesToKnockBack()
    {
        circlePlayerAnimator.SetTrigger("PlayKnockbak");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);

            if (distance <= knockBackDistance)
            {
                enemy.GetComponent<KnockBackEnemy>()?.PlayKnockBack();
                enemy.GetComponent<EnemyHitStop>()?.TriggerHitStop();
            }
        }
    }

    private void LeveUp()
    {
        level++;
        energyRequiredToTheNextLevel = (int)Math.Ceiling(energyRequiredToTheNextLevel * 1.15f);
        cardsGenerator.GenerateCards();
        CardContainer.SetActive(true);
        Time.timeScale = 0;

        SFXManager.instance.PlaySound("LevelUp");
    }

    private void AddEnergy()
    {
        if (energyTotal == energyRequiredToTheNextLevel - 1)
        {
            energyTotal = 0;
            LeveUp();
        }
        else
        {
            energyTotal++;
            energySFX.PlayWithSoundGrouping();
        }

        UpdateEnergySlider();
    }

    public void IncreaseBaseDamage(float increaseMultiplier)
    {
        bulletDamage = (int)Math.Ceiling(bulletDamage * increaseMultiplier);
        orbitalDamage = (int)Math.Ceiling(orbitalDamage * increaseMultiplier);
    }

    private void FormatLuckItems()
    {
        foreach (var item in luckItems)
        {
            for (int i = 0; i < item.chance; i++)
            {
                luckItemsFormated.Add(item.item);
            }
        }
    }

    public bool GenerateLuckItem(Vector3 newPosition)
    {
        if (UnityEngine.Random.Range(0, 100) <= luck)
        {
            GameObject luckItem = luckItemsFormated[UnityEngine.Random.Range(0, luckItemsFormated.Count)];
            var item = Instantiate(luckItem, itemsContainer.transform);
            item.transform.localPosition = newPosition;
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // --- Damages ---
        if (
            (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("BossHeadBullet"))
            && animator.GetBool("isVulnerable")
        )
        {
            float damage = collision.gameObject.CompareTag("Enemy")
                ? enemyDamageOnTouch
                : collision.gameObject.CompareTag("EnemyBullet")
                    ? collision.gameObject.GetComponent<EnemyBullet>().damage
                    : collision.gameObject.GetComponent<BossHeadFire>().damage;
            PlayerLifeManager.Instance.TakeDamage(damage);
            PlayEnemiesToKnockBack();
            return;
        }

        // --- Energy ---
        if (collision.gameObject.CompareTag("Energy"))
        {
            AddEnergy();
            Destroy(collision.gameObject);
            return;
        }

        // --- Items ---
        if (
            collision.gameObject.CompareTag("HealthItem")
            && !PlayerLifeManager.Instance.IsFullHealth()
        )
        {
            SFXManager.instance.PlaySound("Heal");
            PlayerLifeManager.Instance.Heal();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("BombItem"))
        {
            GameObject.Find("EnemiesGenerator").GetComponent<EnemiesGenerator>().KillAllToEnemies();
            collision.gameObject.GetComponent<BombExplotion>().BombExplosion();
            return;
        }

        if (collision.gameObject.CompareTag("MagnetItem"))
        {
            collision.gameObject.GetComponent<MagnetItem>().SuperMagnetize();
            return;
        }
    }
}
