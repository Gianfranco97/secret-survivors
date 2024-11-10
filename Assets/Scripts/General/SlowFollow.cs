using UnityEngine;

public class SlowFollow : MonoBehaviour
{
    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private Transform target;
    public bool isFollowing = true;

    void Update()
    {
        if (isFollowing)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }
}
