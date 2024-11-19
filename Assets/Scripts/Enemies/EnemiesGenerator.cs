using System;
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
    private float timeBetweenBasicEnemies = 2f;
    private float timeBetweenFastEnemies = 4f;
    //private float timeBetweenFireEnemies = 18f;
    private float timeBetweenFireEnemies = 4f;
    private float timeToIncreseDificulty = 30f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        //StartCoroutine(ExecuteEveryInterval(() => GenerateEnemy(enemyBasicPrefab), () => timeBetweenBasicEnemies));
        //StartCoroutine(ExecuteEveryInterval(() => GenerateEnemy(enemyFastPrefab), () => timeBetweenFastEnemies));
        StartCoroutine(ExecuteEveryInterval(() => GenerateEnemy(enemyFirePrefab), () => timeBetweenFireEnemies));
        StartCoroutine(IncreseDificulty());
    }

    private void Update()
    {
        spanwPoint.RotateAround(player.transform.position, new Vector3(0, 0, 1), 400 * Time.deltaTime);
    }

    private void GenerateEnemy(GameObject enemyPrefab)
    {
        enemies.Add(Instantiate(enemyPrefab, spanwPoint.position, Quaternion.identity, transform));
    }

    private IEnumerator ExecuteEveryInterval(Action action, Func<float> getInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(getInterval());
            action();
        }
    }

    private IEnumerator IncreseDificulty()
    {
        return ExecuteEveryInterval(() =>
        {
            timeBetweenBasicEnemies *= 0.65f;
            timeBetweenFastEnemies *= 0.65f;
            timeBetweenFireEnemies *= 0.65f;
        }, () => timeToIncreseDificulty);
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
