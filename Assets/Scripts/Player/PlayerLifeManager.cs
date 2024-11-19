using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private GameObject heartsContainer;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    public GameObject[] hearts;
    public static PlayerLifeManager Instance { get; private set; }
    private Animator playerAnimator;
    [SerializeField] private int maxLife = 3;
    public int invulnerabilityMilliseconds = 1000;
    public LifeManager life;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            life = new LifeManager(maxLife);
            playerAnimator = GetComponent<Animator>();
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        ReacreateHeartsList();
    }

    public void UpdateHearts()
    {
        int fullHearts = (int)life.currentHealth / 2;
        int halfHearts = (int)life.currentHealth % 2;

        for (int i = 0; i < hearts.Length; i++)
        {
            Image heartImage = hearts[i].GetComponent<Image>();

            if (i < fullHearts)
            {
                heartImage.sprite = fullHeart;
            }
            else if (i < fullHearts + halfHearts)
            {
                heartImage.sprite = halfHeart;
            }
            else
            {
                heartImage.sprite = emptyHeart;
            }
        }
    }

    private void ReacreateHeartsList()
    {
        foreach (var heart in hearts)
        {
            Destroy(heart);
        }

        int maxLife = (int)Math.Ceiling(life.maxHealth / 2);

        hearts = new GameObject[maxLife];
        for (int i = 0; i < maxLife; i++)
        {
            hearts[i] = Instantiate(heartPrefab, heartsContainer.transform);
        }

        UpdateHearts();
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        playerAnimator.SetBool("isVulnerable", false);
        yield return new WaitForSeconds(invulnerabilityMilliseconds / 1000f);
        playerAnimator.SetBool("isVulnerable", true);
    }

    public void TakeDamage(float damage)
    {
        /*
        if (playerAnimator.GetBool("isVulnerable"))
        {
            life.TakeDamage(damage);
            UpdateHearts();

            SFXManager.instance.PlaySound("HitPlayer");

            if (life.currentHealth <= 0)
            {
                GameManager.Instance.ShowGameOverMenu();
                return;
            }
            else
            {
                playerAnimator.SetBool("isVulnerable", false);
                StopAllCoroutines();
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.2f, 4f));
                StartCoroutine(InvulnerabilityCoroutine());
            }
        }*/
    }

    public void AddNewHeart()
    {
        life.IncreaseMaxHealth(2);
        ReacreateHeartsList();
    }

    public void FullHealth()
    {
        life.Heal(life.maxHealth);
        ReacreateHeartsList();
    }

    public void Heal(float amount = 2)
    {
        life.Heal(amount);
        ReacreateHeartsList();
    }

    public bool IsFullHealth()
    {
        return life.currentHealth == life.maxHealth;
    }
}
