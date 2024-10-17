using System.Collections;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private IkarugaEnemy ikarugaEnemy;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private float bulletDamage = 1f;
    private GameObject player;


    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<EnemyBullet>().isDark = ikarugaEnemy.isDark;
            bullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - firePoint.position).normalized * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<EnemyBullet>().damage = bulletDamage;

            Destroy(bullet, bulletLifeTime);
        }
    }
}
