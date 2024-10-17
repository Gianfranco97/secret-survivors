using System.Collections;
using UnityEngine;

public class KnockbackEnemy : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PlayerActions player;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerActions>();
    }

    public void PlayKnockback()
    {
        StopAllCoroutines();
        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb2d.AddForce(direction * player.knockbackStrength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(player.knockbackDelay);
        rb2d.velocity = Vector3.zero;
    }
}
