using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHitStop : MonoBehaviour
{
    public float stopDuration = 0.5f; 
    private NavMeshAgent agent;      
    private bool isHitStopActive = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerHitStop()
    {
        if (!isHitStopActive)
        {
            StartCoroutine(HitStopCoroutine());
        }
    }

    private IEnumerator HitStopCoroutine()
    {
        isHitStopActive = true;

        float originalSpeed = agent.speed;

        agent.speed = 0f;
        agent.isStopped = true;

        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;

        yield return new WaitForSecondsRealtime(stopDuration);

        agent.speed = originalSpeed;
        agent.isStopped = false;
        spriteRenderer.color = originalColor;

        isHitStopActive = false;
    }
}
