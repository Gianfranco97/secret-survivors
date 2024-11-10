using UnityEngine;

public class RotateAroundTarget : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 50f;
    private Quaternion[] originalRotations;

    void Start()
    {
        originalRotations = new Quaternion[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            originalRotations[i] = transform.GetChild(i).rotation;
        }
    }

    void Update()
    {
        transform.RotateAround(target.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = originalRotations[i];
        }
    }
}