using UnityEngine;

public class InstantFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = target.position;
    }
}
