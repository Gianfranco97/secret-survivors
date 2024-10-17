using UnityEngine;
using UnityEngine.AI;

public class EnemyBasicMove : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        if (target) agent.SetDestination(target.position);
    }
}
