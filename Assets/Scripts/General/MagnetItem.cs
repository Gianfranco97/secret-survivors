using System.Collections;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    private CircleCollider2D magnetCollider;
    private float actualRadius;
    private float timeToDestroy = 0.1f;
    private float timeToDisableSuperMagnetize = 0.05f;
    private float superSpeed = 450;

    private void Start()
    {
        magnetCollider = GameObject.Find("ObjectMagnet").GetComponent<CircleCollider2D>();
    }

    private void DisableSuperMagnetize()
    {
        magnetCollider.radius = actualRadius;
    }

    private IEnumerator ChangeMagnetSpeed()
    {
        float originalSpeed = MagnetInfo.speed;
        MagnetInfo.speed = superSpeed;
        yield return new WaitForSeconds(1);
        MagnetInfo.speed = originalSpeed;
    }

    public void SuperMagnetize()
    {
        StartCoroutine(ChangeMagnetSpeed());
        SFXManager.instance.PlaySound("Magnet");
        GameObject.Find("Player").GetComponent<PlayerActions>().circlePlayerAnimator.SetTrigger("SuperMagnet");
        actualRadius = magnetCollider.radius;
        magnetCollider.radius = 3000;
        Invoke(nameof(DisableSuperMagnetize), timeToDisableSuperMagnetize);
        Destroy(gameObject, timeToDestroy);
    }
}
