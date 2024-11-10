using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 200f;
    public float timeBetweenShots = 0.4f;
    [SerializeField] private List<GameObject> firePoints;
    [SerializeField] private List<GameObject> firePointsActives;
    [SerializeField] private Transform PupilHomunculus;
    [SerializeField] private PlayerActions player;
    [SerializeField] private VariantsSFX sfx;
    private Vector2 mausePosition;
    private float lastShotTime = 0f;
    private float pupilRadius = 0.8f;

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
        Vector2 lookDir = mausePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        float zRad = (angle + 90) * Mathf.Deg2Rad;
        float x = pupilRadius * Mathf.Cos(zRad);
        float y = pupilRadius * Mathf.Sin(zRad);
        PupilHomunculus.localPosition = new Vector2(x, y);
    }

    void CreateBullet(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z), firePoint.rotation);
        bullet.GetComponent<IkarugaColor>().isDark = player.ikarugaColor.isDark;
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        lastShotTime = Time.time;

        Destroy(bullet, 3f);
    }

    public void Fire()
    {
        if (Time.time >= lastShotTime + timeBetweenShots)
        {
            sfx.PlaySound();
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
