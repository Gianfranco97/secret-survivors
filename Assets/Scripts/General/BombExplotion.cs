using UnityEngine;

public class BombExplotion : MonoBehaviour
{
    [SerializeField] private Animator expansionWave;
    [SerializeField] private float timeToDestroy = 0.2f;

    public void Explotion()
    {
        expansionWave.SetTrigger("Explotion");
        Destroy(gameObject, timeToDestroy);
    }
}
 