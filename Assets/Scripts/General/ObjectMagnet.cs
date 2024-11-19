using UnityEngine;

public class ObjectMagnet : MonoBehaviour
{
    private bool magnetOn = false;
    private float speed = 150;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectMagnet"))
        {
            magnetOn = true;
        }
    }

    private void Update()
    {
        if (magnetOn)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Player").transform.position, MagnetInfo.speed * Time.deltaTime);
        }
    }
}
