using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHitStop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float stopDuration = 0.2f;
    private NavMeshAgent agent;
    private bool isHitStopActive = false;
    private Material originalMaterial;
    private Material whiteFlashMaterial;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalMaterial = spriteRenderer.material;
        whiteFlashMaterial = Resources.Load<Material>("Materials/FlashMaterial");
    }

    public void TriggerHitStop()
    {
        if (!isHitStopActive)
        {
            StartCoroutine(HitStopSpriteCoroutine());
            StartCoroutine(HitStopAgentCoroutine());
        }
    }

    private IEnumerator HitStopAgentCoroutine()
    {
        if (agent == null || !agent.isActiveAndEnabled) yield break;

        isHitStopActive = true;

        var originalSpeed = agent.speed;
        var originalAcceleration = agent.acceleration;
        var originalAngularSpeed = agent.angularSpeed;

        agent.speed = 0f;
        agent.acceleration = 0f;
        agent.angularSpeed = 0f;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        yield return new WaitForSecondsRealtime(stopDuration);

        agent.speed = originalSpeed;
        agent.acceleration = originalAcceleration;
        agent.angularSpeed = originalAngularSpeed;
        agent.isStopped = false;

        isHitStopActive = false;
    }

    private IEnumerator HitStopSpriteCoroutine()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.material = whiteFlashMaterial;
        spriteRenderer.color = Color.white;

        yield return new WaitForSecondsRealtime(stopDuration);

        spriteRenderer.material = originalMaterial;
        spriteRenderer.color = originalColor;
    }
}
