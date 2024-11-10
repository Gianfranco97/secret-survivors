using System.Collections;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private IkarugaColor ikarugaColor;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float firstFireDelay = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private float bulletDamage = 1f;
    private PlayerActions player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerActions>();
        StartCoroutine(Fire());
    }

    private GameObject SetBulletValues(GameObject bullet)
    {
        bool isRegularEnemyBullet = bulletPrefab.name == "EnemyFireBullet";
        var enemyBullet = isRegularEnemyBullet ? bullet.GetComponent<EnemyBullet>() : bullet.GetComponentInChildren<BossHeadFire>();
        var bulletBody = isRegularEnemyBullet ? bullet.GetComponent<Rigidbody2D>() : bullet.GetComponentInChildren<Rigidbody2D>();

        enemyBullet.ikarugaColor.isDark = ikarugaColor.isDark;
        enemyBullet.damage = bulletDamage;
        bulletBody.AddForce((player.transform.position - firePoint.position).normalized * bulletSpeed, ForceMode2D.Impulse);
        enemyBullet.gameObject.GetComponentInChildren<Animator>()?.SetBool("isDark", ikarugaColor.isDark);

        return bullet;
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(firstFireDelay);

        while (true)
        {
            GameObject bullet = SetBulletValues(Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            if (bulletPrefab.name == "BossFireBullet")
            {
                SFXManager.instance.PlaySound("BossAttack");
            }

            Destroy(bullet, bulletLifeTime);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
