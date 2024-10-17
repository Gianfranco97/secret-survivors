using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyBasicPrefab;
    [SerializeField] private GameObject enemyFastPrefab;
    [SerializeField] private GameObject enemyFirePrefab;
    [SerializeField] private Transform spanwPoint;
    public List<GameObject> enemies;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(GenerateEnemies());
    }

    private void Update()
    {
        spanwPoint.RotateAround(player.transform.position, new Vector3(0, 0, 1), 400 * Time.deltaTime);
    }

    private void GenerateEnemy(GameObject enemyPrefab)
    {
        enemies.Add(Instantiate(enemyPrefab, spanwPoint.position, Quaternion.identity, transform));
    }

    private IEnumerator GenerateEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            GenerateEnemy(enemyBasicPrefab);
            yield return new WaitForSeconds(1f);
            GenerateEnemy(enemyFastPrefab);
            yield return new WaitForSeconds(1f);
            GenerateEnemy(enemyFirePrefab);
        }
    }

    private void DestroyAllTheBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    public void KillAllToEnemies()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
        DestroyAllTheBullets();
    }
}
