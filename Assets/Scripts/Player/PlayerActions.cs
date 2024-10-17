using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public float knockbackStrength = 16, knockbackDelay = 0.15f, knockbackDistance = 10;
    public int level = 0, energyTotal = 0, energyRequiretToTheNextLevel = 20;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI levelText;
    public float speed = 10f;
    [SerializeField] private float enemyDamageOnTouch = 2f;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private int invulnaberabilityMilliseconds = 1000;
    [SerializeField] private Animator knockbackCircleAnimator;
    [SerializeField] private GameObject CardContainer;
    [SerializeField] private CardsGenerator cardsGenerator;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 moveSpeed;
    public bool isDark = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateEnergySlider();
    }


    void Update()
    {
        ProcessInputs();
        IkarugaColor();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void IkarugaColor()
    {
        spriteRenderer.color = isDark ? Color.red : Color.green;
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDark = !isDark;
        }
    }

    void MovePlayer()
    {
        moveSpeed = moveDirection * speed;
        rb.velocity = new Vector2(moveSpeed.x, moveSpeed.y);
    }

    void MakeVulnerable()
    {
        animator.SetBool("isVulnerable", true);
    }

    private void UpdateEnergySlider()
    {
        energySlider.maxValue = energyRequiretToTheNextLevel;
        energySlider.value = energyTotal;
        levelText.text = $"Level: {level}";
    }

    private void PlayEnemiesToKnockback()
    {
        knockbackCircleAnimator.SetTrigger("PlayKnockbak");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);

            if (distance <= knockbackDistance)
            {
                enemy.GetComponent<KnockbackEnemy>().PlayKnockback();
            }
        }
    }

    private void LeveUp()
    {
        level++;
        energyRequiretToTheNextLevel = (int)Math.Ceiling(energyRequiretToTheNextLevel * 1.15f);
        cardsGenerator.GenerateCards();
        CardContainer.SetActive(true);
        Time.timeScale = 0;
    }

    private void AddEnergy()
    {
        if (energyTotal == energyRequiretToTheNextLevel - 1)
        {
            energyTotal = 0;
            LeveUp();
        }
        else
        {
            energyTotal++;
        }

        UpdateEnergySlider();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("Enemy")
            && animator.GetBool("isVulnerable")
            && collision.gameObject.GetComponent<IkarugaEnemy>().isDark != isDark
        )
        {
            PlayerLifeManager.Instance.TakeDamage(enemyDamageOnTouch);
            animator.SetBool("isVulnerable", false);
            Invoke(nameof(MakeVulnerable), invulnaberabilityMilliseconds / 1000);
            PlayEnemiesToKnockback();
            return;
        }

        if (collision.gameObject.CompareTag("Energy"))
        {
            AddEnergy();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("HealthItem"))
        {
            PlayerLifeManager.Instance.Heal();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("BombItem"))
        {
            GameObject.Find("EnemiesGenerator").GetComponent<EnemiesGenerator>().KillAllToEnemies();
            collision.gameObject.GetComponent<BombExplotion>().Explotion();
            return;
        }

        if (collision.gameObject.CompareTag("MagnetItem"))
        {
            collision.gameObject.GetComponent<MagnetItem>().SuperMagnetize();
            return;
        }
    }
}
