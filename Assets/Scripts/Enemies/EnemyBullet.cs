using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 1f;
    public IkarugaColor ikarugaColor;
    public PlayerActions player;

    private void Start()
    {
        GameObject playerGameObject = GameObject.Find("Player");
        player = playerGameObject.GetComponent<PlayerActions>();

        ikarugaColor = playerGameObject.GetComponent<IkarugaColor>();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
