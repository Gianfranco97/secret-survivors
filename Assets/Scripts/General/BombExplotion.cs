using UnityEngine;

public class BombExplotion : MonoBehaviour
{
    [SerializeField] private Animator expansionWave;
    [SerializeField] private float timeToDestroy = 0.2f;

    public void BombExplosion()
    {
        SFXManager.instance.PlaySound("Boom");
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        StartCoroutine(cameraShake.Shake(1f, 2f));
        expansionWave.SetTrigger("BombExplosion");
        Destroy(gameObject, timeToDestroy);
    }
}
