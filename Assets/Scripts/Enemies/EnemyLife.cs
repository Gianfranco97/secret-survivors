using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private GameObject energyPrefab;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private List<GameObject> luckItems;
    private EnemiesCounter enemiesCounter;
    private LifeManager life;
    private EnemyHitStop hitStop;
    public float luck = 10;

    private void Start()
    {
        hitStop = GetComponent<EnemyHitStop>();
        life = new LifeManager(maxHealth);
        enemiesCounter = GameObject.Find("GameManager").GetComponent<EnemiesCounter>();
    }

    private void GenerateEnergy()
    {
        Instantiate(energyPrefab, transform.position, Quaternion.identity);
    }

    private void IncreaseCounter()
    {
        enemiesCounter.AddDefeatEnemy(enemyType, GetComponent<IkarugaEnemy>().isDark);
        enemiesCounter.UpdateCounterTexts();
    }

    private void GenerateLuckItem()
    {
        Instantiate(luckItems[Random.Range(0, luckItems.Count)], transform.position, Quaternion.identity);
        /*
        if (Random.Range(0, 100) <= luck)
        {
            Instantiate(luckItems[Random.Range(0, luckItems.Count)], transform);
        }
        */
    }

    public void TakeDamage(int damage)
    {
        life.TakeDamage(damage);

        if (life.currentHealth <= 0)
        {
            GenerateEnergy();
            IncreaseCounter();
            GenerateLuckItem();
            Destroy(gameObject);
        }
        else
        {
            hitStop.TriggerHitStop();
        }
    }
}
