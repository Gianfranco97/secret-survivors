using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    public float timeBetweenShots = 0.4f;
    [SerializeField] private List<GameObject> firePoints;
    [SerializeField] private List<GameObject> firePointsActives;
    [SerializeField] private PlayerActions player;
    [SerializeField] Rigidbody2D playerBody;
    private Vector2 mausePosition;
    private float lastShotTime = 0f;

    private void Update()
    {
        ProcessInputs();
        RotatePlayer();
    }

    private void ProcessInputs()
    {
        mausePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    void RotatePlayer()
    {
        Vector2 lookDir = mausePosition - playerBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerBody.rotation = angle;
    }

    void CreateBullet(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z), firePoint.rotation);
        bullet.GetComponent<BulletActions>().isDark = player.isDark;
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        lastShotTime = Time.time;

        Destroy(bullet, 2f);
    }

    public void Fire()
    {
        if (Time.time >= lastShotTime + timeBetweenShots)
        {
            foreach (var firePoint in firePointsActives)
            {
                CreateBullet(firePoint.transform);
            }
        }
    }

    public void ActiveFirePoint()
    {
        var newFirePoint = firePoints[firePointsActives.Count];
        newFirePoint.SetActive(true);
        firePointsActives.Add(newFirePoint);
    }
}
