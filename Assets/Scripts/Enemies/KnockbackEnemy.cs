using System.Collections;
using UnityEngine;

public class KnockBackEnemy : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PlayerActions player;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerActions>();
    }

    public void PlayKnockBack()
    {
        StopAllCoroutines();
        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb2d.AddForce(direction * player.knockBackStrength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(player.knockBackDelay);
        rb2d.velocity = Vector3.zero;
    }
}