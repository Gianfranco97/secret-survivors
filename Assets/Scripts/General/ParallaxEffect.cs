using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float parallaxSpeed = 0.02f;
    private Transform cameraTransform;
    private float startPostionX;
    private float startPostionY;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        startPostionX = transform.position.x;
        startPostionY = transform.position.y;
    }

    private void Update()
    {
        float distX = (cameraTransform.position.x * parallaxSpeed);
        float distY = (cameraTransform.position.y * parallaxSpeed);
        transform.position = new Vector3(startPostionX + distX, startPostionY + distY, transform.position.z);
    }
}
